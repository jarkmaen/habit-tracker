CREATE TABLE Habits (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    CurrentStreak INT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETUTCDATE()
);

CREATE TABLE Completions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    HabitId INT NOT NULL,
    CompletedDate DATE NOT NULL,
    FOREIGN KEY (HabitId) REFERENCES Habits(Id),
    UNIQUE(HabitId, CompletedDate)
);
