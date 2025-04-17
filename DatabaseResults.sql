USE Bookstore
GO

-- Question 3: returns the Book Title of books written by Stephen King
SELECT DISTINCT b.Title
	FROM Book AS b
	LEFT JOIN Book_Author AS ba ON b.BookId = ba.BookId
	LEFT JOIN Author AS a ON ba.AuthorId = a.AuthorId
WHERE a.LastName = 'King' AND a.FirstName = 'Stephen';

-- Question 4: Bonus question – returns the Book Title of books written by only Stephen King
SELECT DISTINCT b.Title
	FROM Book AS b
	LEFT JOIN Book_Author AS ba ON b.BookId = ba.BookId
	LEFT JOIN Author AS a ON ba.AuthorId = a.AuthorId
WHERE a.LastName = 'King' AND a.FirstName = 'Stephen'
	AND b.BookId IN (SELECT ba.BookId FROM Book_Author AS ba GROUP BY ba.BookId HAVING COUNT(*) = 1);