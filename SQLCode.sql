SELECT * FROM Projects

--INSERT INTO Projects (ProjectName, StartDate, EndDate, CustomerId, StatusId, UserId, ProductId) 
--VALUES ('Test Project', '2024-01-01', '2024-12-31', 1, 1, 1, 2);


SELECT p.ProjectName, p.StartDate, p.EndDate, c.CustomerId, st.StatusId, u.UserId, pro.ProductId
FROM Projects p
JOIN Customers c on c.Id = p.Id
JOIN StatusTypes st on st.Id = p.Id
JOIN Users u on u.Id = p.Id
JOIN Products pro on pro.Id = p.Id
