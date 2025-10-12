-------------------------------------------- Standalone CTE ------------------------------------------------
-- CTEs (Common Table Expressions) are temporary named result sets used only within the following query.
-- You can define multiple CTEs by separating them with commas.

WITH
CTE_TotalSales AS (
    -- Calculates total sales per customer
    SELECT CustomerID, SUM(Sales) AS TotalSales
    FROM Sales.Orders
    GROUP BY CustomerID
),

CTE_LastOrder AS (
    -- Gets the latest order date for each customer
    SELECT CustomerID, MAX(OrderDate) AS LastOrder
    FROM Sales.Orders
    GROUP BY CustomerID
)

-- Use the two CTEs by joining them to the Customers table
SELECT
    c.CustomerID,
    c.FirstName,
    c.LastName,
    cts.TotalSales,
    clo.LastOrder
FROM Sales.Customers AS c
LEFT JOIN CTE_TotalSales AS cts ON c.CustomerID = cts.CustomerID
LEFT JOIN CTE_LastOrder AS clo ON c.CustomerID = clo.CustomerID;