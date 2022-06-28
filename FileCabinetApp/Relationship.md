------------------------------------------------------------------------------------------
|          Class 1          |          Class 2          |          Relationship          | 
|---------------------------|---------------------------|--------------------------------|
| FileCabinetService        | IRecordValidator          | Агрегация                      |
| FileCabinetDefaultService | FileCabinetService        | Наследование                   |
| FileCabinetCustomService  | FileCabinetService        | Наследование	                 |
| FileCabinetDefaultService | DefaultValidator          | Ассоциация                     |
| FileCabinetCustomService  | CustomValidator           | Ассоциация                     |
| DefaultValidator          | IRecordValidator          | Наследование                   |
| CustomValidator           | IRecordValidator          | Наследование                   |
------------------------------------------------------------------------------------------