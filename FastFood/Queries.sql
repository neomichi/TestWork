a)
SELECT Emp."Name",Emp."Salary"
FROM "Employee" Emp
LEFT JOIN "Employee" Chief ON Chief."Id" = Emp."ChiefId"
WHERE Emp."Salary">Chief."Salary"

b)
SELECT Emp."Name",Emp."Salary"  FROM "Employee" Emp
JOIN 
(
SELECT Dep."Id",Max(Emp."Salary")as mxs
FROM "Employee" Emp
INNER JOIN "Department" Dep ON Dep."Id" = Emp."DepartmentId"
GROUP BY Dep."Id"
) t
on Emp."Salary" = t.mxs and Emp."DepartmentId" = t."Id"
GROUP BY Emp."Name",Emp."Salary";

c)

SELECT Dep."Name",Count(*)
FROM "Employee" Emp
INNER JOIN "Department" Dep ON Dep."Id" = Emp."DepartmentId"
GROUP BY Dep."Id"
HAVING Count(*) <= 3

d)

SELECT DISTINCT Emp."Name"
FROM "Employee" Emp
inner JOIN "Employee" Chief ON Chief."Id" = Emp."ChiefId"
WHERE Emp."DepartmentId"!=Chief."DepartmentId"
e) 


SELECT Dep."Name",Sum(Emp."Salary") as sum
FROM "Employee" Emp
INNER JOIN "Department" Dep ON Dep."Id" = Emp."DepartmentId"
GROUP BY Dep."Id"
Order by sum DESC
f)


4) создать таблицу аналалогично Employee + поле CreateAT NotNull для хранения времени
5) создать таблицу  "EmployeeDepartment"  {EmployeeId,DepartmentId} + (композитный уникальный индекс на два поля), 
создать тригер, в котором проверять условие Employee.DepartmentId!={EmployeeId,DepartmentId}.DepartmentId где innerjoin EmployeeId
