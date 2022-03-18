-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE SEQUENCE books_seq;

CREATE TABLE books (
  id INT DEFAULT NEXTVAL ('books_seq') PRIMARY KEY,
  author text,
  launch_date timestamp(6) NOT NULL,
  price decimal(65,2) NOT NULL,
  title text
) ;