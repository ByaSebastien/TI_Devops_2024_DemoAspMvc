CREATE TABLE [dbo].[Book]
(
	[ISBN] VARCHAR(13) PRIMARY KEY,
	[Title] VARCHAR(100) NOT NULL,
	[Description] VARCHAR(MAX) NULL,
	[Publish_date] DATETIME2 NOT NULL,
	[Author_id] INT NOT NULL,
	CONSTRAINT FK_Book_Author FOREIGN KEY (Author_id)
		REFERENCES Author(Id),
	CONSTRAINT UK_Book UNIQUE (Title,Publish_date,Author_id)
)
