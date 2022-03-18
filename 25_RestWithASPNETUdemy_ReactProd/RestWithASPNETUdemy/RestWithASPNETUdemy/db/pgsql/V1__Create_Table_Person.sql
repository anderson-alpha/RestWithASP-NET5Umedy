-- SQLINES LICENSE FOR EVALUATION USE ONLY
CREATE SEQUENCE person_seq;

CREATE TABLE person (
  id int NOT NULL DEFAULT NEXTVAL ('person_seq'),
  first_name varchar(80) NOT NULL,
  last_name varchar(80) NOT NULL,
  address varchar(100) NOT NULL,
  gender varchar(6) NOT NULL,
  PRIMARY KEY (id)
) 