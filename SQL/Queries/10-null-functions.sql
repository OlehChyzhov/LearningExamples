----------------------------------------------ISNULL--------------------------------------------------
-- Replaces 'NULL' with a specified value. Limitet to two values. Fast

SELECT
AVG(Score) AS average_score_wrong,
AVG(ISNULL(Score, 0)) AS average_score_correct
FROM Sales.Customers;

----------------------------------------------COALESCE--------------------------------------------------
-- Returns the first non-null value from a list. Unlimited. Slow

SELECT CustomerID, FirstName, LastName,
FirstName + ' ' + COALESCE(LastName, '') AS FullName,
Score,
COALESCE(Score, 0) + 10 AS score_with_bonus
FROM Sales.Customers;

----------------------------------------------NULLIF--------------------------------------------------
-- Compares two expressions. Returns NULL if the are equal. First Value, if they are not equal

SELECT OrderID, Sales, Quantity,
Sales/NULLIF(Quantity, 0) AS Price -- Useful for preventing dividing by zero
FROM Sales.Orders;