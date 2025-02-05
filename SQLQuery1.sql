SELECT * FROM Projects

SELECT 
	p.ProjectName, p.StartDate, p.EndDate, c.CustomerName, st.StatusName, u.FirstName, pro.ProductName
FROM Projects p
JOIN Customers c on c.Id = p.CustomerId  -- Foreign key
JOIN StatusTypes st on st.Id = p.StatusId  -- Foreign key
JOIN Users u on u.Id = p.UserId  -- Foreign key
JOIN Products pro on pro.Id = p.ProductId  -- Foreign key

