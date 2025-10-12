-----------------------------------------NOT-----------------------------------------
-- Rows where condition is NOT true
SELECT * 
FROM customers
WHERE NOT country = 'USA';

----------------------------------------BETWEEN--------------------------------------
-- Rows where value is within a range (inclusive)
SELECT * 
FROM customers 
WHERE score BETWEEN 300 AND 600;

--------------------------------------IN---------------------------------------------
-- Value matches any in the list (cleaner than multiple ORs)
SELECT * 
FROM customers 
WHERE country IN ('USA', 'Germany');

--------------------------------------NOT IN-----------------------------------------
-- Value does NOT match any in the list
SELECT * 
FROM customers 
WHERE country NOT IN ('USA', 'Germany');

-----------------------------------------LIKE----------------------------------------
-- Value contains 'r' anywhere
SELECT * 
FROM customers 
WHERE first_name LIKE '%r%';

-- Value has 'r' as the third character
SELECT * 
FROM customers 
WHERE first_name LIKE '__r%';
