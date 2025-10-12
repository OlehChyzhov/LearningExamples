---------------------------------------CONCAT----------------------------------------
-- Combine strings into one value
SELECT
	first_name,
	country,
	CONCAT(first_name, '-', country) AS name_country
FROM customers;

------------------------------------UPPER & LOWER------------------------------------
-- Convert text to upper or lower case
SELECT 
	first_name, UPPER(first_name) AS first_name_upper,
	country, LOWER(country) AS country_lower,
	score
FROM customers;

----------------------------------------TRIM-----------------------------------------
-- Remove leading/trailing spaces
SELECT 
	first_name, 
	LEN(first_name) - LEN(TRIM(first_name)) AS trimmed_spaces
FROM customers;

---------------------------------------REPLACE---------------------------------------
-- Replace part of a string with something else
SELECT *, 
	REPLACE(order_date, '-', '.') AS replaced_date 
FROM orders;

-------------------------------------LEFT & RIGHT------------------------------------
-- Get first or last N characters
SELECT
	first_name,
	LEFT(TRIM(first_name), 2) AS first_2_char,
	RIGHT(first_name, 2) AS last_2_char
FROM customers;

--------------------------------------SUBSTRING--------------------------------------
-- Extract part of a string starting from a given position
SELECT 
	first_name,
	SUBSTRING(TRIM(first_name), 4, LEN(first_name)) AS sub_name
FROM customers;

-------------------------------------ROUND & ABS-------------------------------------
-- ROUND: Round a number to N decimal places
SELECT 
	3.516 AS original, 
	ROUND(3.516, 2) AS round_2_decimals, 
	ROUND(3.516, 1) AS round_1_decimal, 
	ROUND(3.516, 0) AS round_0_decimals;

-- ABS: Return absolute (positive) value
SELECT 
	-228 AS original, 
	ABS(-228) AS absolute_value;