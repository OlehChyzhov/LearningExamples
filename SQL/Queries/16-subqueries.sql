------------------------------------------------ Subquery Result Types ------------------------------------------------
SELECT AVG(Sales) FROM Sales.Orders; -- Scalar query (Single value)
SELECT CustomerID FROM Sales.Orders; -- Row query (One column)
SELECT * FROM Sales.Orders; -- Table query (Multiple columns and rows)


-------------------------------------------- Subquery in FROM ------------------------------------------------
-- Using a subquery (derived table) to calculate the average price alongside each product.
-- The window function AVG(Price) OVER () computes the average over the entire Products table.
-- The outer query filters only those products whose price is greater than the average.

SELECT *
FROM 
    (SELECT *,
    AVG(Price) OVER () AS [AVGPrice]  -- Same value repeated for all rows
    FROM Sales.Products) AS t
WHERE Price > AVGPrice;


-------------------------------------------- Subquery in SELECT ------------------------------------------------
-- Only scalar subqueries (returning a single value) are allowed in the SELECT list

SELECT ProductID, [Product], Price,
(SELECT COUNT(*) FROM Sales.Orders) AS TotalOrders
FROM Sales.Products;


-------------------------------------------- Subquery in JOIN ------------------------------------------------
-- Use a subquery as a derived table in a JOIN. Here, we join Customers to total order counts per customer.

SELECT c.*, o.TotalOrders
FROM Sales.Customers AS c
LEFT JOIN (
    SELECT 
    CustomerID,
    COUNT(*) AS TotalOrders
    FROM Sales.Orders
    GROUP BY CustomerID) AS o
ON c.CustomerID = o.CustomerID;


-------------------------------------------- Subquery in WHERE ------------------------------------------------
-- When using comparison operators, only scalar subqueries are allowed

SELECT ProductID, Price
FROM Sales.Products
WHERE Price > (SELECT AVG(Price) FROM Sales.Products);


-------------------------------------------- Subquery using IN Operator ------------------------------------------------
-- Subquery returns a list of values. IN checks if CustomerID is in that list.

SELECT *
FROM Sales.Orders
WHERE CustomerID IN (
    SELECT CustomerID
    FROM Sales.Customers
    WHERE Country = 'Germany');


-------------------------------------------- Subquery using ANY/ALL Operator ------------------------------------------------
-- ANY returns true if the condition is true for ANY of the returned values.
-- ALL returns true only if the condition is true for ALL returned values.

SELECT * FROM Sales.Employees
WHERE
Gender = 'F' AND
Salary > ANY (SELECT Salary FROM Sales.Employees WHERE Gender = 'M');