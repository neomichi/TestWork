INSERT INTO public."Department" ("Id", "Name") VALUES (1, 'Головное подразделение');
INSERT INTO public."Department" ("Id", "Name") VALUES (2, 'Департамент ИТ');
INSERT INTO public."Department" ("Id", "Name") VALUES (3, 'Бухгалтерия');
INSERT INTO public."Department" ("Id", "Name") VALUES (4, 'Админы');

INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (1, 1, null, 'Директор', 100000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (2, 1, 1, 'Секретарша', 10000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (3, 1, 1, 'Уборщица', 8000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (4, 2, 1, 'ИТ директор', 70000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (5, 2, 4, 'Руководитель разработки', 50000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (6, 2, 4, 'Руководитель тестирования', 50000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (7, 2, 5, 'Ведущий разработчик', 80000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (8, 2, 5, 'Backend разработчик', 38000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (9, 2, 5, 'Frontend разработчик', 38000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (10, 2, 5, 'Junior разработчик', 20000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (11, 2, 6, 'Ведущий тестировщик', 70000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (12, 2, 6, 'Тестировщик', 30000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (13, 3, 1, 'Главбух', 120000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (14, 3, 13, 'Бухгалтер', 30000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (15, 3, 13, 'Младший бухгалтер', 30000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (16, 4, 1, 'Главный админ', 38000.00);
INSERT INTO public."Employee"("Id", "DepartmentId", "ChiefId", "Name", "Salary") VALUES (17, 4, 16, 'Младший админ', 25000.00);