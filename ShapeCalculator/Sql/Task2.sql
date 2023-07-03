CREATE TABLE Categories (
    Id INT PRIMARY KEY,
    Name VARCHAR(255)
);

CREATE TABLE Products (
    Id INT PRIMARY KEY,
    Name VARCHAR(255)
);

CREATE TABLE ProductsToCategories (
    CategoryId INT,
    ProductId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

select [Categories].[Name] as 'Categories', [Products].[Name]  as 'Products'
from [Categories]
	inner join [ProductsToCategories]
		on [Categories].[Id] = [ProductsToCategories].[CategoryId]
	right join [Products]
		on [ProductsToCategories].[ProductId] = [Products].[Id]