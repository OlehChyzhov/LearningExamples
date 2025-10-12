USE MyDatabase;
-------------------------------------------- Aggregate Functions --------------------------------------------
-- Aggregate functions summarize multiple rows into a single result (used in GROUP BY or alone)
SELECT 
       COUNT(*)     AS [total_nr_orders],   -- Total number of rows (orders)
       SUM(Sales)   AS [total_sales],       -- Total sales amount
       AVG(Sales)   AS [avg_sales],         -- Average sales value
       MAX(Sales)   AS [highest_sales],     -- Highest sale value
       MIN(Sales)   AS [lowest_sales]       -- Lowest sale value
FROM Orders;


USE SalesDB;
--------------------------------------------- Window Functions ----------------------------------------------
-- Window (analytic) functions operate over a set of rows related to the current row.
-- Unlike aggregates, they return a value for every row — without collapsing the result set.

SELECT 
       OrderID,
       ProductID,
       OrderDate,
       OrderStatus,
       Sales,
       -- OVER() keeps all rows and calculates the SUM() for all rows (Entire data counts as one window).
       -- PARTITION BY ProductID works like GROUP BY. It groups rows by coumns (Creates different windows).
       SUM(Sales) OVER () AS [TotalSales],
       SUM(Sales) OVER (PARTITION BY ProductID) AS [TotalSalesByProduct],
       SUM(Sales) OVER (PARTITION BY ProductID, OrderStatus) AS [SalesByProductAndStatus]
FROM Sales.Orders;


-- Window ORDER BY
SELECT OrderID, OrderDate, Sales,
       -- RANK() assigns a rank based on the sorted order of Sales.
       -- ORDER BY inside OVER() defines how ranking is determined (descending sales here).
       -- If multiple rows have the same Sales value, they receive the same rank (with gaps).
       RANK() OVER (ORDER BY Sales DESC) AS [RankSales]
FROM Sales.Orders;


------------------------------------------------ Window Frame ------------------------------------------------
-- Window Frame:
-- Defines the subset of rows (relative to the current row) to include in the window function calculation.
-- This example:
--  PARTITION BY OrderStatus: Restart calculation for each OrderStatus group
--  ORDER BY OrderDate: Sort rows chronologically within each partition
--  ROWS BETWEEN CURRENT ROW AND 2 FOLLOWING: Include the current and next 2 rows in the sum

SELECT 
       OrderID, 
       OrderDate, 
       OrderStatus, 
       Sales,

       SUM(Sales) OVER (
           PARTITION BY OrderStatus 
           ORDER BY OrderDate 
           ROWS BETWEEN CURRENT ROW AND 2 FOLLOWING
       ) AS [TotalSales]
FROM Sales.Orders;