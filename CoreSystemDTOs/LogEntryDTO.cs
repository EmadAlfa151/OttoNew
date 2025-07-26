
    public class LogEntryDTO
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; } = string.Empty;
        public int LogLevelId { get; set; }
        public int LogTypeId { get; set; }
    }
