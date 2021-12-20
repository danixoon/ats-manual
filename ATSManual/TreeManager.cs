using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ATSManual.Database;

namespace ATSManual
{

    public class SubscribersManager
    {
        public class ViewItem
        {
            public DataGridViewRow Item { get; private set; }
            public ATSDataSet.SubscriberDataRow subscriber;
            public IEnumerable<ATSDataSet.DataRow> data;
            public ViewItem(ATSDataSet.SubscriberDataRow subscriber, IEnumerable<ATSDataSet.DataRow> data)
            {
                this.subscriber = subscriber;
                this.data = data;
                this.Item = new DataGridViewRow()
                {
                    Tag = this,
                };
                this.Item.Cells.Add(new DataGridViewTextBoxCell() { Value = subscriber.phone });
                this.Item.Cells.Add(new DataGridViewTextBoxCell() { Value = subscriber.subscriberName });
            }
            public override string ToString()
            {
                return $"{subscriber.phone} - {subscriber.subscriberName}";
            }
        }
        public DataGridView view { get; private set; }
        public List<ViewItem> items { get; private set; } = new List<ViewItem>();
        public Action<ViewItem> painter = (p) => { };

        public SubscribersManager(DataGridView view, Action<ViewItem> painter)
        {
            this.view = view;
            this.painter = painter;
        }

        public DataGridView FillView(List<ViewItem> items)
        {

            this.view.Rows.Clear();
            this.items = items.Select(item =>
            {
                painter(item);
                item.Item.Selected = false;
                return item;
            }).ToList();


            view.Rows.AddRange(this.items.Select(item =>
            {
                return item.Item;
            }).ToArray());

            return view;
        }

        public void ClearSelection()
        {
            //foreach (var item in this.items)
            //{
            //    item.Item.Selected = false;
            //}
            view.ClearSelection();
        }

        public void ScrollToSelected()
        {
            List<ViewItem> items = new List<ViewItem>();
            foreach (DataGridViewRow row in view.SelectedRows)
                items.Add(row.Tag as ViewItem);

            if (items.Count == 0) return;
            //if (items. > 1) return;
            //foreach (var item in items)
            //{
            //    if (item.Item.Index == -1 || !item.Item.Selected) continue;
            //items.Sort((a, b) => a.subscriber.phone > b.subscriber.phone ? 1 : -1);
            var item = items.FirstOrDefault();

            var visible = view.Rows.GetRowCount(DataGridViewElementStates.Displayed);
            var index = view.FirstDisplayedScrollingRowIndex;
            var diff = item.Item.Index - index;
            if (diff < 0 || diff > visible)
            {
                var nextIndex = item.Item.Index - visible / 2;
                view.FirstDisplayedScrollingRowIndex = nextIndex < visible ? visible / 2 : nextIndex;
                return;
            }
            //}
        }

        public void SelectItems(params ViewItem[] items)
        {
            view.ClearSelection();
            foreach (var item in items)
            {
                if (item.Item.Index == -1) continue;
                item.Item.Selected = true;
            }
        }
    }
}

public class TreeManager
{

    public class TreeItem
    {
        // ИД группы (-1, если элемент)
        public int id;
        // ИД отца (-1, если корень)
        public int parentId;
        public string name;

        public TreeNode node;

        public TreeItem(string name, int id, int parentId, TreeNode node = null)
        {
            this.name = name;
            this.id = id;
            this.parentId = parentId;
            this.node = node ?? new TreeNode(name);
            this.node.Tag = this;
        }
    }

    public TreeView tree { get; private set; }
    public List<TreeItem> items { get; private set; } = new List<TreeItem>();

    public Action<TreeItem> painter = (p) => { };

    public TreeManager(TreeView tree, Action<TreeItem> painter)
    {
        this.tree = tree;
        this.painter = painter;
    }

    public List<TreeItem> FindChilds(int rootId, Dictionary<int, List<TreeItem>> items)
    {
        // Находит всех детей корня
        var childs = items.Values.SelectMany(item => item).ToList().FindAll(item => rootId == item.parentId);
        return childs;
    }
    public IEnumerable<TreeNode> SlideTree(int rootId, Dictionary<int, List<TreeItem>> items)
    {
        List<TreeNode> nodes = new List<TreeNode>();

        while (items[rootId].Count > 0)
        {
            var root = items[rootId].First();
            items[rootId].Remove(root);

            // Если это группа (присутствуют потомки)
            if (root.id != -1)
            {
                nodes.Add(root.node);

                var childs = FindChilds(root.id, items);

                foreach (var child in childs)
                {
                    SlideTree(child.id, items);
                }

                root.node.Nodes.AddRange(childs.Select(child => child.node).ToArray());
            }
        }

        return nodes;
    }


    private List<TreeItem> FindChilds(int id, List<TreeItem> items)
    {
        return items.FindAll(item => item.parentId == id);
    }

    private List<TreeItem> GoDown(TreeItem root, List<TreeItem> items)
    {
        var nodes = new List<TreeItem>();
        if (root.id != -1)
        {
            var childs = FindChilds(root.id, items);
            items.RemoveAll(item => childs.Contains(item));

            foreach (var child in childs)
            {
                var innerChilds = GoDown(child, items);
            }

            root.node.Nodes.AddRange(childs.Select(child => child.node).ToArray());

            nodes = childs;
        }

        return nodes;
    }

    private TreeItem FindParent(TreeItem start, List<TreeItem> items)
    {
        var up = items.Find(item => item.id != -1 && item.id == start.parentId);
        items.Remove(up);
        return up;
    }

    public IEnumerable<TreeItem> FillTree(List<TreeItem> items)
    {
        this.tree.Nodes.Clear();
        this.items = items.Select(item =>
        {
            painter(item);
            return item;
        }).ToList();

        var tree = this.BuildTree(new List<TreeItem>(items), null);
        this.tree.Nodes.AddRange(tree.Select(item => item.node).ToArray());

        return tree;
    }

    private IEnumerable<TreeItem> BuildTree(List<TreeItem> items, TreeItem root = null)
    {
        if (items.Count == 0) return new List<TreeItem>();

        root = items.First();
        items.Remove(root);

        var downs = GoDown(root, items);
        if (items.Count > 0)
        {
            var parent = FindParent(root, items);
            if (parent != null)
            {
                return BuildTree(items, parent);
            }
            else
            {
                var result = new List<TreeItem>();
                result.Add(root);
                result.AddRange(BuildTree(items, items.First()));
                return result;
            }
        }
        else
            return new List<TreeItem>() { root };
    }

    public delegate bool TreeFilterPredicate(TreeItem item);

    public IEnumerable<TreeItem> SearchTree(TreeFilterPredicate predicate)
    {
        return this.items.FindAll(item => predicate(item));
    }

    public void SortTree(Comparison<TreeItem> comparer)
    {
        foreach (var item in this.items)
        {
            var nodes = new List<TreeNode>();
            foreach (var node in nodes)
                nodes.Add(node);

            nodes.Sort((a, b) => comparer((TreeItem)a.Tag, (TreeItem)b.Tag));
            item.node.Nodes.Clear();
            item.node.Nodes.AddRange(nodes.ToArray());
        }
        //if(root == null)
        //{
        //    foreach(var node in tree.Nodes)
        //    {

        //    }
        //}
    }
}
