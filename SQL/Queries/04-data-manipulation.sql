SELECT * FROM customers;
SELECT * FROM people;

----------------------------------------INSERT---------------------------------------
-- Insert rows with specified columns
INSERT INTO customers (id, first_name, country, score)
VALUES
	(6, 'Anna', 'USA', NULL),
	(7, 'Sam', NULL, 100);

-- Insert row with all columns (must match table structure)
INSERT INTO customers
VALUES (8, 'Andreas', 'Germany', NULL);

-- Insert row with partial columns; others get default/NULL
INSERT INTO customers (id, first_name)
VALUES (9, 'Sahra');

-- Insert from SELECT (copy data from one table to another)
INSERT INTO people(id, person_name, birth_date, phone)
SELECT id, first_name, NULL, 'Unknown' 
FROM customers;

----------------------------------------UPDATE---------------------------------------
-- Update single column
UPDATE customers
SET score = 0
WHERE id = 6;

-- Update multiple columns
UPDATE customers
SET score = 0,
    country = 'UK'
WHERE id = 9;

-- Update all rows where condition is met
UPDATE customers
SET score = 0
WHERE score IS NULL;

----------------------------------------DELETE---------------------------------------
SELECT * FROM customers;
SELECT * FROM people;

-- Delete rows based on condition
DELETE FROM customers
WHERE id > 5;

-- Delete all rows (slower)
DELETE FROM people;

-- Delete all rows (faster)
TRUNCATE TABLE people;