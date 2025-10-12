-- Use the database
USE MyDatabase;

----------------------------------------NO JOIN--------------------------------------
-- Select all rows from each table separately
SELECT * FROM customers;
SELECT * FROM orders;

--------------------------------------INNER JOIN-------------------------------------
-- Rows where LEFT and RIGHT tables have matching values
SELECT customers.id, first_name, country, score, order_id, order_date, sales 
FROM customers
INNER JOIN orders ON orders.customer_id = customers.id;

--------------------------------------LEFT JOIN--------------------------------------
-- All rows from LEFT + matching rows from RIGHT
SELECT * FROM customers
LEFT JOIN orders ON orders.customer_id = customers.id;

--------------------------------------RIGHT JOIN-------------------------------------
-- All rows from RIGHT + matching rows from LEFT
SELECT * FROM customers
RIGHT JOIN orders ON orders.customer_id = customers.id;

--------------------------------------FULL JOIN--------------------------------------
-- All rows from LEFT and RIGHT
SELECT * FROM customers
FULL JOIN orders ON orders.customer_id = customers.id;

--------------------------------------LEFT ANTI JOIN----------------------------------
-- Only rows from LEFT with no match in RIGHT
SELECT * FROM customers
LEFT JOIN orders ON customers.id = orders.customer_id
WHERE orders.customer_id IS NULL;

--------------------------------------RIGHT ANTI JOIN---------------------------------
-- Only rows from RIGHT with no match in LEFT
SELECT * FROM customers
RIGHT JOIN orders ON customers.id = orders.customer_id
WHERE customers.id IS NULL;

--------------------------------------FULL ANTI JOIN----------------------------------
-- Unmatched rows from both LEFT and RIGHT
SELECT * FROM customers
FULL JOIN orders ON customers.id = orders.customer_id
WHERE customers.id IS NULL OR orders.customer_id IS NULL;

--------------------------------------CROSS JOIN--------------------------------------
-- All combinations of LEFT and RIGHT rows (Cartesian product)
SELECT * FROM customers
CROSS JOIN orders;

-----------------------------------MULTIPLE TABLE JOINS-----------------------------------
-- Use a different database
USE SalesDB;

-- Join three tables: LEFT (Orders), MIDDLE (Customers), RIGHT (Products)
SELECT OrderDate, ShipDate, OrderStatus, ShipAddress, BillAddress, 
       Quantity, Sales, CreationTime, FirstName, LastName, Country, Score, Category, Price
FROM Sales.Orders AS o
JOIN Sales.Customers AS c ON o.CustomerID = c.CustomerID
JOIN Sales.Products AS p ON o.ProductID = p.ProductID;
