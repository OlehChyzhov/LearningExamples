USE SalesDB;
---------------------------------SET Rules & Syntax----------------------------------
SELECT -- Number of columns and data types have to match 
FirstName AS [Name], -- First query controls column names
LastName AS Surname -- First query controls column names
FROM Sales.Customers

UNION -- Returns all district rows from both queries (Removes dublicates)

SELECT -- Number of columns and data types have to match
FirstName AS BIBA, -- First query controls column names
LastName
FROM Sales.Employees
ORDER BY FirstName; -- "ORDER BY" only in the end


--------------------------------------UNION ALL--------------------------------------
SELECT FirstName, LastName
FROM Sales.Customers

-- Faster than UNION
UNION ALL -- Returns all rows from both queries (Does NOT remove dublicates)

SELECT FirstName, LastName
FROM Sales.Employees;

---------------------------------------EXCEPT----------------------------------------
-- Returns all distinct rows from the first query that are not found in the second query

SELECT FirstName, LastName
FROM Sales.Customers
EXCEPT -- Customers that are not Employees
SELECT FirstName, LastName
FROM Sales.Employees;


SELECT FirstName, LastName
FROM Sales.Employees
EXCEPT -- Employees that are not Customers
SELECT FirstName, LastName
FROM Sales.Customers;

--------------------------------------INTERSECT--------------------------------------
-- Returns only the rows that are common in both queries. Removes dublicates

SELECT FirstName, LastName
FROM Sales.Employees
INTERSECT
SELECT FirstName, LastName
FROM Sales.Customers;