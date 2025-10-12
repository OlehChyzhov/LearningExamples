-- A Stored Procedure is a precompiled block of SQL code used for reusable logic, complex operations, or data manipulation.
-- It helps improve performance, reduces code duplication, and enhances security by controlling data access.

---------------------------------------- Simple Stored Procedure Example ----------------------------------------
CREATE PROCEDURE GetCustomerSummary AS
BEGIN
	SELECT
		COUNT(*) AS TotalCustomers,
		AVG(Score) AS AverageScore
	FROM Sales.Customers
	WHERE Country = 'USA'
END;

EXEC GetCustomerSummary;


---------------------------------------- Stored Procedure with parameters ----------------------------------------
CREATE PROCEDURE GetCustomerSummary 
@Country NVARCHAR(50) = 'USA' -- Default Parameter
AS
BEGIN
	SELECT
		COUNT(*) AS TotalCustomers,
		AVG(Score) AS AverageScore
	FROM Sales.Customers
	WHERE Country = @Country
END;

EXEC GetCustomerSummary @Country = 'USA';


---------------------------------------- Variables ----------------------------------------
ALTER PROCEDURE GetCustomerSummary @Country NVARCHAR(50) = 'USA' AS
BEGIN
	DECLARE @TotalCustomers INT, @AvgScore FLOAT; -- Variables Declaration
	SELECT
		-- Variable Assignment
		@TotalCustomers = COUNT(*),
		@AvgScore = AVG(Score)
	FROM Sales.Customers
	WHERE Country = @Country

	PRINT 'Total Customers from ' + @Country + ': ' + CAST(@TotalCustomers AS NVARCHAR);
	PRINT 'Average Score from ' + @Country + ': ' + CAST(@AvgScore AS NVARCHAR);
END;

EXEC GetCustomerSummary @Country = 'Germany';


---------------------------------------- Triggers ----------------------------------------
-- Special stored procedure that automatically runs in response to a specific event on a table or view
-- Trigger types:
-- DML Triggers - INSERT, UPDATE, DELETE. There are triggers that run after the event and during the event
-- DDL Triggers - CREATE, ALTER, DROP

CREATE TRIGGER trg_AfterInsertEmployee ON Sales.Employees
AFTER INSERT
AS
BEGIN
	INSERT INTO Sales.EmployeeLogs (EmployeeID, LogMessage, LogDate)
	SELECT 
	EmployeeID,
	'New Employee Added =' + CAST(EmployeeID AS VARCHAR),
	GETDATE()
	FROM INSERTED -- This is virtual table that holds a copy of the rows that are being inserted into the target table
END


INSERT INTO Sales.Employees VALUES (6, 'Maria', 'Doe', 'HR', '1988-01-12', 'F', 80000, 3);
SELECT * FROM Sales.EmployeeLogs;