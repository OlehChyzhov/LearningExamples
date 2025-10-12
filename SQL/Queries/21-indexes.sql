/*
-------------------------------------
Clustered Index:
-------------------------------------
1. Rows are sorted based on the column the clustered index is created on.
2. Creates a B-TREE structure:
   - Data Pages -> Leaf nodes (store the actual data).
   - Index Pages -> Intermediate nodes (store key ranges and pointers to the next level in the tree, helping quickly locate data).

-------------------------------------
Clustered Index Structure (B-Tree):
-------------------------------------
            [Root Index Page]
                   |
                1:300
                   |
            -------------------
           |                   |
       [Index Page]       [Index Page]
           1:200               1:201
        (Keys 1–10)         (Keys 11–20)
           |                   |
      ------------        ------------
     |            |      |            |
 [Data Page]  [Data Page] [Data Page] [Data Page]
   1:100        1:101       1:102       1:103
 (Keys 1–5)   (Keys 6–10)  (Keys 11–15) (Keys 16–20)


- Index Pages: Intermediate nodes (store key ranges & pointers).
- Data Pages: Leaf nodes (store actual data rows).


Heap - Table without a clustered index

-------------------------------------
Non-Clustered Index:
-------------------------------------

1. The actual data pages are not changed (rows remain in their original order).
2. It creates index pages that store:
   - The indexed column values (sorted).
   - Pointers (addresses) to the actual rows in the data pages (these pointers are stored at the leaf level).
3. The B-Tree structure is otherwise the same as in the clustered index,
   but the leaves contain references to data rows instead of the data itself.
*/

-- CREATE [CLUSTERED | NONCLUSTERED] INDEX index_name ON table_name (column1, column2, ...)


----------------------------------- CLUSTERED and NONCLUSTERED Indexes ---------------------------------------

-- Creates a clustered index on CustomerID (physically sorts data by CustomerID)
CREATE CLUSTERED INDEX idx_DbCustomers_CustomerID ON Sales.DbCustomers (CustomerID);

-- Creates a non-clustered index on LastName (creates separate index structure with pointers to data)
CREATE NONCLUSTERED INDEX idx_DbCustomers_LastName ON Sales.DbCustomers (LastName);

-- Uses non-clustered index on LastName to locate rows with 'Brown'
SELECT *
FROM Sales.DbCustomers
WHERE LastName = 'Brown';


--------------------------------------- Composite Index ---------------------------------------

-- Creates composite index on (Country, Score) (first sorted by Country, then by Score)
CREATE INDEX idx_DbCustomers_CountryScore ON Sales.DbCustomers (Country, Score);

-- Query uses both columns in the same order as the index is created
SELECT *
FROM Sales.DbCustomers
WHERE Country = 'USA' AND Score > 500;

-- This query also uses the index (the leftmost column 'Country' is included)
SELECT *
FROM Sales.DbCustomers
WHERE Country = 'USA';


--------------------------------------- Columnstore Index ---------------------------------------
-- Rowstore indexes (the default type) store all columns for a specific row together in data pages.
-- Columnstore indexes store each column separately in dedicated data pages (one page per column).
-- Columnstore indexes are designed to improve performance of large analytical queries by allowing 
-- the engine to read only the necessary columns instead of scanning entire rows.
-- Does not create B-Tree and requires less space by compretion

CREATE CLUSTERED COLUMNSTORE INDEX idx_DbCustomers_CS ON Sales.DbCustomers


--------------------------------------- Unique Index ---------------------------------------
-- A Unique Index ensures that all values in the indexed column(s) are unique.
-- It automatically rejects INSERT or UPDATE operations that would cause duplicates.
-- Useful for enforcing business rules like unique emails, usernames, etc.

-- Can't create unique index when table contains duplicates
CREATE UNIQUE NONCLUSTERED INDEX idx_Products_Category ON Sales.Products (Product)

-- Cant insert new value if it is duplicate
INSERT INTO Sales.Products (ProductID, Product) VALUES (106, 'Caps')


--------------------------------------- Filtered Index ---------------------------------------
-- An index that works only on rows meeting the specified conditions
-- Can't create a filtered index on both clustered and columnstore indexes

CREATE NONCLUSTERED INDEX idx_Customers_Country ON Sales.Customers (Country)
WHERE Country = 'USA'

-- This will work faster
SELECT * FROM Sales.Customers WHERE Country = 'USA'


/*
    When To Use Each Index Type:

    Heap:
    - Use when you need fast, bulk INSERT operations.
    - No clustered index exists; data is stored in no particular order.

    Clustered Index:
    - Use for Primary Keys or main lookup columns.
    - If no ID exists, pick another column that makes sense for ordering (e.g., Date).

    Columnstore Index:
    - Use for large tables with Analytical Queries.
    - Great for compression and reducing storage size.

    Non-Clustered Index:
    - Use for secondary lookups (e.g., Foreign Keys, JOINs, WHERE filters).
    - Can include specific columns to cover queries.

    Filtered Index:
    - Use to index a subset of data (e.g., WHERE IsActive = 1).
    - Reduces index size and improves performance on targeted queries.

    Unique Index:
    - Enforces data uniqueness (e.g., emails, usernames).
    - Also improves lookup speed on the unique column(s).
*/