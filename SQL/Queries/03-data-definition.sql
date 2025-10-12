-------------------------------------CREATE TABLE------------------------------------
-- Create a table with column definitions and a primary key
CREATE TABLE people (
	id INT NOT NULL,
	person_name VARCHAR(50) NOT NULL,
	birth_date DATE,
	phone VARCHAR(15) NOT NULL,
	CONSTRAINT pk_people PRIMARY KEY(id)
);

-------------------------------------ALTER TABLE-------------------------------------
-- Add a new column
ALTER TABLE people
ADD email VARCHAR(50) NOT NULL;

-- Remove a column
ALTER TABLE people
DROP COLUMN phone;

-------------------------------------DROP TABLE--------------------------------------
-- Delete the entire table and its data
DROP TABLE people;