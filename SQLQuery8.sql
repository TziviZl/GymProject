-- 1. מחיקת ה-Primary Key הקיים (כי אי אפשר לשנות עמודה עם מפתח ראשי)
ALTER TABLE GymnastClasses DROP CONSTRAINT PK_GymnastClasses;

-- 2. שינוי העמודה Id ל-IDENTITY
ALTER TABLE GymnastClasses
ALTER COLUMN Id INT IDENTITY(1,1) NOT NULL;

-- 3. יצירת מפתח ראשי מחדש
ALTER TABLE GymnastClasses
ADD CONSTRAINT PK_GymnastClasses PRIMARY KEY CLUSTERED (Id);
