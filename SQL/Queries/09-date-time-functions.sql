------------------------------------------ DAY / MONTH / YEAR ------------------------------------------
-- Extract year, month, and day as separate columns using basic date functions
SELECT OrderID, CreationTime,
       YEAR(CreationTime) AS [year],
       MONTH(CreationTime) AS [month],
       DAY(CreationTime) AS [day]
FROM Sales.Orders;

--------------------------------------------- DATEPART -------------------------------------------------
-- Extract numeric parts of the datetime. All results are integers.
-- WEEKDAY: 1 = Sunday by default (can vary depending on language settings)
SELECT OrderID, CreationTime,
       DATEPART(YEAR, CreationTime) AS [year],
       DATEPART(MONTH, CreationTime) AS [month],
       DATEPART(QUARTER, CreationTime) AS [quarter],
       DATEPART(DAY, CreationTime) AS [day],
       DATEPART(WEEK, CreationTime) AS [week],
       DATEPART(WEEKDAY, CreationTime) AS [week_day],
       DATEPART(HOUR, CreationTime) AS [hour],
       DATEPART(MINUTE, CreationTime) AS [minute],
       DATEPART(SECOND, CreationTime) AS [second]
FROM Sales.Orders;

--------------------------------------------- DATENAME -------------------------------------------------
-- Extract textual names of date parts (e.g., "January", "Monday").
-- YEAR returns "2025" as a string; MONTH returns "July"; WEEKDAY returns "Thursday", etc.
SELECT OrderID, CreationTime,
       DATENAME(YEAR, CreationTime) AS [year],
       DATENAME(MONTH, CreationTime) AS [month],
       DATENAME(WEEKDAY, CreationTime) AS [week_day]
FROM Sales.Orders;

-------------------------------------------- DATETRUNC -------------------------------------------------
-- Truncates the datetime to the start of the specified part.
-- Useful for grouping or aligning times to clean intervals.
-- Returns a full datetime value (with lower parts zeroed).
SELECT OrderID, CreationTime,
       DATETRUNC(YEAR,   CreationTime) AS [year],        -- '2025-01-01 00:00:00.000'
       DATETRUNC(MONTH,  CreationTime) AS [month],       -- '2025-07-01 00:00:00.000'
       DATETRUNC(WEEK,   CreationTime) AS [week_start],  -- Week start (usually Monday)
       DATETRUNC(HOUR,   CreationTime) AS [hour],        -- e.g., '2025-07-10 15:00:00.000'
       DATETRUNC(MINUTE, CreationTime) AS [minute],      -- e.g., '2025-07-10 15:22:00.000'
       DATETRUNC(SECOND, CreationTime) AS [second]       -- e.g., '2025-07-10 15:22:18.000'
FROM Sales.Orders;

-- Group orders by the beginning of each month to summarize monthly order counts
SELECT 
       DATETRUNC(MONTH, OrderDate)   AS [month_trunc_date], 
       DATENAME(MONTH, OrderDate)    AS [month_name], 
       COUNT(OrderID)                AS [orders_per_month]
FROM Sales.Orders
GROUP BY DATETRUNC(MONTH, OrderDate);

--------------------------------------------- EOMONTH --------------------------------------------------
-- Get the last date of the month for the given date (time part will be 00:00:00)
SELECT OrderID, CreationTime,
       EOMONTH(CreationTime) AS [month_end]
FROM Sales.Orders;

---------------------------------------------- FORMAT --------------------------------------------------
-- Format DATE or TIME values as strings using .NET-style format specifiers.
-- Slower than DATEPART/DATENAME, but useful for UI formatting or exporting text.
SELECT OrderID, CreationTime,
       FORMAT(CreationTime, 'dd') AS [dd],         -- Day (01–31)
       FORMAT(CreationTime, 'ddd') AS [ddd],       -- Abbreviated day name (e.g., "Mon")
       FORMAT(CreationTime, 'dddd') AS [dddd],     -- Full day name (e.g., "Monday")

       FORMAT(CreationTime, 'MM') AS [MM],         -- Month number (01–12)
       FORMAT(CreationTime, 'MMM') AS [MMM],       -- Abbreviated month name (e.g., "Jul")
       FORMAT(CreationTime, 'MMMM') AS [MMMM],     -- Full month name (e.g., "July")

       FORMAT(CreationTime, 'yy') AS [yy],         -- Two-digit year (e.g., "25")
       FORMAT(CreationTime, 'yyy') AS [yyy],       -- Rare/legacy (same as 'yyyy')
       FORMAT(CreationTime, 'yyyy') AS [yyyy],     -- Full year (e.g., "2025")

       FORMAT(CreationTime, 'hh') AS [hh],         -- Hour in 12-hour format (01–12)
       FORMAT(CreationTime, 'HH') AS [HH],         -- Hour in 24-hour format (00–23)

       FORMAT(CreationTime, 'm') AS [m],           -- Minute (0–59, no leading 0)
       FORMAT(CreationTime, 'mm') AS [mm],         -- Minute (always two digits)

       FORMAT(CreationTime, 's') AS [s],           -- Second (0–59, no leading 0)
       FORMAT(CreationTime, 'ss') AS [ss],         -- Second (always two digits)

       FORMAT(CreationTime, 'f') AS [f],           -- First fractional second digit (e.g., .1 from .123)
       FORMAT(CreationTime, 'ff') AS [ff],         -- Two fractional second digits (e.g., .12)
       FORMAT(CreationTime, 'fff') AS [fff],       -- Milliseconds (e.g., .123)

       FORMAT(CreationTime, 'tt') AS [tt]          -- AM/PM designator
FROM Sales.Orders;

--------------------------------------------- CONVERT --------------------------------------------------
-- Convert values between types. Optional 3rd param = style code (for date formatting).
SELECT 
       CONVERT(INT, '123') AS [string_to_int],
       CONVERT(DATE, '2025-07-10') AS [string_to_date],
       CONVERT(VARCHAR, CreationTime, 32) AS [us_format_style_32]  -- Format: mm/dd/yyyy hh:mi:ss AM/PM
FROM Sales.Orders;

----------------------------------------------- CAST ---------------------------------------------------
-- Convert value to another data type (same as CONVERT, but simpler syntax, no style codes).
SELECT
       CAST('342' AS INT) AS [string_to_int],
       CAST('2025-07-10' AS DATE) AS [string_to_date],
       CAST(123 AS VARCHAR) AS [int_to_string];

--------------------------------------------- DATEADD --------------------------------------------------
-- Add or subtract units of time from a date.
SELECT OrderID, OrderDate,
       DATEADD(YEAR, 1, OrderDate) AS [next_year],
       DATEADD(MONTH, -1, OrderDate) AS [previous_month],
       DATEADD(DAY, 22, OrderDate) AS [in_22_days]
FROM Sales.Orders;

--------------------------------------------- DATEDIFF -------------------------------------------------
-- Get the difference between two dates in specified units.
-- Returns number of unit-boundaries crossed, not precise durations.
SELECT OrderID, OrderDate, ShipDate,
       DATEDIFF(YEAR, OrderDate, ShipDate) AS [year_diff],
       DATEDIFF(MONTH, OrderDate, ShipDate) AS [month_diff],
       DATEDIFF(DAY, OrderDate, ShipDate) AS [day_diff]
FROM Sales.Orders;

---------------------------------------------- ISDATE --------------------------------------------------
-- Returns 1 if the input is a valid date, 0 otherwise.
-- Useful for validating string inputs before converting to DATE or DATETIME.
SELECT 
       ISDATE('2025') AS [valid_1],           -- 1 (interpreted as '2025-01-01')
       ISDATE('2025-07-10') AS [valid_2],     -- 1 (ISO date)
       ISDATE('123') AS [valid_3];            -- 0 (not a valid date)
