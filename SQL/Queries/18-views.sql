-- Views are stored SQL queries in the database.
-- They act like virtual tables and can be queried just like regular tables.
-- This view shows a monthly summary of sales activity.

CREATE VIEW Sales.V_MonthlySummary AS
(
	SELECT
		DATETRUNC(MONTH, OrderDate) AS OrderMonth,      -- Truncate date to the first day of the month
		SUM(Sales) AS TotalSales,                        -- Total sales amount for the month
		COUNT(OrderID) AS TotalOrders,                   -- Number of orders in the month
		SUM(Quantity) AS TotalQuantities                 -- Total quantity of items sold in the month
	FROM Sales.Orders
	GROUP BY DATETRUNC(MONTH, OrderDate)
);

-- Drop the view when it is no longer needed
DROP VIEW Sales.V_MonthlySummary;
