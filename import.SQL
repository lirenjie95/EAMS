/*
Navicat Premium Data Transfer
Source Server		: localhost
Source Server Type	: SQL Server 2014
Source Host		: localhost
Source Database		: HW
File Encoding		: utf-8
*/

-- Grade_to_GPA: e.g. A -> 4.0
IF object_id('Grade_to_GPA','U') IS NOT NULL
DROP TABLE Grade_to_GPA;
CREATE TABLE Grade_to_GPA(
	Grade CHAR(2) PRIMARY KEY,
	GPA REAL
);
INSERT INTO Grade_to_GPA VALUES('A',4.0);
INSERT INTO Grade_to_GPA VALUES('A-',3.7);
INSERT INTO Grade_to_GPA VALUES('B+',3.3);
INSERT INTO Grade_to_GPA VALUES('B',3.0);
INSERT INTO Grade_to_GPA VALUES('B-',2.7);
INSERT INTO Grade_to_GPA VALUES('C+',2.3);
INSERT INTO Grade_to_GPA VALUES('C',2.0);
INSERT INTO Grade_to_GPA VALUES('C-',1.7);
INSERT INTO Grade_to_GPA VALUES('D',1.3);
INSERT INTO Grade_to_GPA VALUES('D-',1.0);
INSERT INTO Grade_to_GPA VALUES('F',0.0);
INSERT INTO Grade_to_GPA VALUES('P',NULL);

-- 
IF object_id('Student','U') IS NOT NULL
DROP TABLE Student;
