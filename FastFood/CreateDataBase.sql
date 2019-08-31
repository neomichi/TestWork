CREATE TABLE public."Department"
(
  "Id" integer NOT NULL DEFAULT nextval('dep_sequence'::regclass),
  "Name" character(100),
  CONSTRAINT "Department_pkey" PRIMARY KEY ("Id")
)

CREATE TABLE public."Employee"
(
  "Id" integer primary key DEFAULT nextval('emp_sequence'::regclass),
  "Name" character(200),
  "Salary" float,
  "ChiefId" integer ,
  "DepartmentId" integer,
   CONSTRAINT "Employee_Department_key" FOREIGN KEY ("DepartmentId")
      REFERENCES public."Department" ("Id"),
   CONSTRAINT "Employee_Chief_key" FOREIGN KEY ("ChiefId")
      REFERENCES public."Employee" ("Id")
);

CREATE TABLE public."EmployeeHistory"
(
  "Id" integer  primary key DEFAULT nextval('dep_sequence'::regclass),
  "Name" character(100),
  "DepartmentId" integer NOT NULL,
  "ChiefId" integer NOT NULL,
  "Salary" float NOT NULL,
  "CreateAt" datatime NOT NULL,
  "EmployeeId" integer NOT NULL,
   CONSTRAINT "EmployeeHistory_key" FOREIGN KEY ("DepartmentId")
      REFERENCES public."Department" ("Id"),
   CONSTRAINT "Employee_key" FOREIGN KEY ("EmployeeId")
      REFERENCES public."Employee" ("Id")
  
  CONSTRAINT "EmployeeHistory_pkey" PRIMARY KEY ("Id")
)




///не забывайте про dep_sequence,emp_sequence, employeeHistory_pkey

