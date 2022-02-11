public static class FileCabinetService
{
    private static readonly List<FileCabinetRecord> List = new List<FileCabinetRecord>();

    public static int CreateRecord(string firstName, string lastName, DateTime dateOfBirth)
    {
        var record = new FileCabinetRecord
        {
            Id = List.Count + 1,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
        };

        List.Add(record);

        return record.Id;
    }

    public static FileCabinetRecord[] GetRecords()
    {
        // TODO: добавьте реализацию метода
        return Array.Empty<FileCabinetRecord>();
    }

    public static int GetStat()
    {
        return List.Count;
    }
}