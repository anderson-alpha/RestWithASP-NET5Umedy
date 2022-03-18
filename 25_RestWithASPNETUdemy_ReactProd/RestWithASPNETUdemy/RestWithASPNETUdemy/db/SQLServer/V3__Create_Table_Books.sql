-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE TABLE books (
  id INT IDENTITY PRIMARY KEY,
  author varchar(max),
  launch_date datetime NOT NULL,
  price decimal(18,2) NOT NULL,
  title varchar(max)
) ;