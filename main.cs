 public C1DataCollection<LogDetailModel> GetLogDescriptionDetails2(LogDetailsFilterDTO filterDTO)
 {
     string text = filterDTO.Description;

     List<string[]> parts = new List<string[]>();
     var lines = text.Split('\n');

     foreach (var line in lines)
     {
         parts.Add(line.Split(new[] { ':' }, 2));
     }

     DataTable dataTable = new DataTable();
     DataColumn id = new DataColumn("ID", Type.GetType("System.Int32"));

     id.Unique = true; // столбец будет иметь уникальное значение
     id.AllowDBNull = false; // не может принимать null
     id.AutoIncrement = true; // будет автоинкрементироваться
     id.AutoIncrementSeed = 1; // начальное значение
     id.AutoIncrementStep = 1; // приращении при добавлении новой строки

     DataColumn name = new DataColumn("_ColumnName", Type.GetType("System.String"));
     DataColumn desc = new DataColumn("_ColumnDesc", Type.GetType("System.String"));
     DataColumn value = new DataColumn("_ColumnValue", Type.GetType("System.String"));
     DataColumn comment = new DataColumn("_ColumnComment", Type.GetType("System.String"));
     DataColumn refTable = new DataColumn("_RefTable", Type.GetType("System.String"));
     DataColumn refDB = new DataColumn("_RefDB", Type.GetType("System.String"));

     dataTable.Columns.Add(id);
     dataTable.Columns.Add(name);
     dataTable.Columns.Add(comment);
     dataTable.Columns.Add(value);
     dataTable.Columns.Add(desc);
     dataTable.Columns.Add(refTable);
     dataTable.Columns.Add(refDB);

     foreach (var part in parts)
     {
         dataTable.Rows.Add(new object[] {null, null, part[0], null, part[1], null, null }) ;
     }

     var entities = new ObservableCollection<LogDetailModel>(dataTable.AsEnumerable()
         .Select(item => _logDetailMapper.Map(item)));

     C1DataCollection<LogDetailModel> logsInfoDesc = new C1DataCollection<LogDetailModel>(entities);

     return logsInfoDesc;
 }
