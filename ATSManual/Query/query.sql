--SELECT DISTINCT dep.name AS departmentName, dat.name AS dataName, sub.phone, dat.[key], dep.id as departmentId, dep.parentDepartmentId FROM [Data] dat 
--	RIGHT OUTER JOIN Department dep ON dep.id = dat.departmentId
--	LEFT OUTER JOIN SubscriberData subData ON subData.dataId = dat.id
--	LEFT OUTER JOIN Subscriber sub ON sub.id = subData.subscriberId


--SELECT dep.id as departmentId, dep.name, sub.phone FROM Department dep
--	RIGHT JOIN [Data] dat ON dat.departmentId = dep.id
--	LEFT OUTER JOIN SubscriberData subData ON subData.dataId = dat.id
--	RIGHT JOIN Subscriber sub ON sub.id = subData.subscriberId


--SELECT DISTINCT dep.name AS departmentName, sub.name AS subscriberName, sub.phone, dat.[key], dep.id AS departmentId, dep.parentDepartmentId FROM Subscriber AS sub
-- LEFT JOIN SubscriberData AS subDat ON subDat.subscriberId = sub.id
-- LEFT JOIN [Data] AS dat ON dat.id = subDat.dataId 
-- RIGHT JOIN Department AS dep ON dep.id = sub.departmentId

--SELECT DISTINCT dep.name AS departmentName, sub.name FROM Department dep
--	LEFT OUTER JOIN Subscriber sub ON sub.departmentId = dep.id

-- SELECT * FROM Subscriber

--SELECT dat.id FROM Data AS dat INNER JOIN SubscriberData AS subDat ON subDat.dataId = dat.id
-- WHERE CharIndex(',' + Cast(subDat.subscriberId AS Varchar(10))+',', ',1, 2,') > 0


--INSERT INTO [SubscriberData] ([subscriberId], [dataId], [description]) VALUES 
--	()

--ALTER DATABASE CURRENT COLLATE Cyrillic_General_CI_AS

--DELETE [SubscriberData];
--DELETE [Profile];
--DELETE [Subscriber];
--DELETE [Data];
--DELETE [Department];
