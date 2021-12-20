//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ATSManual.State
//{

//    //public class State<T> where T : ICloneable
//    //{
//    //    public T current { get; set; }
//    //}
//    //public class StateManager<A, T> where T : ICloneable
//    //{
//    //    public delegate T Reducer(T state, A action);
//    //    public List<Reducer> reducers = new List<Reducer>();

//    //    private State<T> _currentState = new State<T>();
//    //    public T currentState
//    //    {
//    //        get
//    //        {
//    //            return this._currentState.current;
//    //        }
//    //        private set { this._currentState.current = value; }
//    //    }

//    //    public event EventHandler<A> OnAction = delegate { };

//    //    public StateManager(params Reducer[] reducers)
//    //    {
//    //        this.reducers = reducers.ToList();
//    //    }

//    //    public void PushAction(A action)
//    //    {
//    //        this.OnAction(this, action);
//    //        var state = this.currentState;
//    //        foreach (var reducer in this.reducers)
//    //        {
//    //            state = reducer(this.currentState, action);
//    //        }
//    //        this.currentState = (T)state.Clone();
//    //    }
//    //}

//    //public class Owo
//    //{
//    //    public enum Actions
//    //    {
//    //        Init
//    //    }
//    //    public struct Info : ICloneable
//    //    {
//    //        public string username;
//    //        public string password;

//    //        public object Clone()
//    //        {
//    //            return new Info() { username = this.username, password = this.password };
//    //        }
//    //    }
//    //    public void Test()
//    //    {
//    //        var manager = new StateManager<Actions, Info>((s, a) =>
//    //        {
//    //            switch (a)
//    //            {
//    //                case Actions.Init:
//    //                    s.password = "234";
//    //                    return s;
//    //                default:
//    //                    return s;
//    //            }
//    //        });

//    //        manager.PushAction(Actions.Init);


//    //    }
//    //}


//    public struct Info : ICloneable
//    {
//        public string username;
//        public string password;

//        public object Clone()
//        {
//            return new Info() { username = this.username, password = this.password };
//        }
//    }

//    public class KState
//    {
//        public class Store<A>
//        {
//            // Cоздать объект с данными хранилища
//            //public 
//            public event EventHandler OnChange = delegate { };
//            public event EventHandler<object> OnDispatch = delegate { };
//            public void Dispatch(object action)
//            {
//                this.OnDispatch(this, action);
//            }
//        }

//        public enum DataState
//        {
//            Loading,
//            Success,
//            Error
//        }

//        public class BindingData<T>
//        {
//            private T _data;
//            public T data
//            {
//                get
//                {
//                    return this._data;
//                }
//                set
//                {
//                    this.isDirty = true;
//                    _data = value;
//                }
//            }
//            public DataState state;
//            public event EventHandler<T> OnChange = delegate { };

//            private bool isDirty = false;
//            public BindingData(EventHandler onDispatch)
//            {
//                onDispatch += (o, e) =>
//                {
//                    this.isDirty = false;
//                    this.OnChange(o, this.data);
//                };
//            }
//        }


//        public class State<T, A>
//        {
//            public T value { get; private set; }
//            public event EventHandler<State<T, A>> OnChange = delegate { };
//            public State(T defaultValue, Store<A> store)
//            {
//                this.value = defaultValue;
//                store.OnChange += (o, e) => this.OnChange(this, this);
//            }
//        }
//        public class DispatchAction<A>
//        {
//            public A type { get; set; }
//            public object payload { get; set; }
//            public DispatchAction(A type, object payload)
//            {
//                this.type = type;
//                this.payload = payload;
//            }
//        }

//        public enum ActionTypes
//        {
//            SelectUser,
//            DeselectUser
//        }

//        public class SelectUserAction : DispatchAction<ActionTypes>
//        {
//            public struct Payload
//            {
//                public string username;
//            }
//            public SelectUserAction(string username) : base(ActionTypes.SelectUser, new Payload() { username = username }) { }
//        }

//        public class Mains
//        {

//            public event EventHandler onDispatch = delegate { };
//            public void Start()
//            {
//                var data = new BindingData<string>(this.onDispatch);

//                var store = new Store<ActionTypes>();

//                store.OnDispatch += (o, e) =>
//                {
//                    var action = (DispatchAction<ActionTypes>)e;
//                    switch (action.type)
//                    {
//                        case ActionTypes.SelectUser:
//                            var payload = (SelectUserAction.Payload)action.payload;
//                            break;
//                    }
//                };

//                var infoState = new State<Info, ActionTypes>(new Info() { password = "123", username = "danya" }, store);

//                store.Dispatch(new SelectUserAction("vasya"));
//            }
//        }
//    }

//    public class TState
//    {
        

//        //public class Ttest
//        //{
//        //    public State<Info> state = new State<Info>(async () =>
//        //    {
//        //        await Task.Delay(100);
//        //        return new Info();
//        //    });

//        //    public async void Mains()
//        //    {
//        //        var state = await this.state.GetState("info", "meme");

//        //    }
//        //}

//    }
//}
