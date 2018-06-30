CREATE TABLE [dbo].[result]
(
	[id_result] INT NOT NULL PRIMARY KEY IDENTITY, 
    [note] DECIMAL(18, 2) NOT NULL, 
    [evaluation_id] INT NOT NULL, 
    [student_id] INT NOT NULL, 
    CONSTRAINT [FK_Result_evaluation] FOREIGN KEY ([evaluation_id]) REFERENCES [evaluation]([id_evaluation]),
    CONSTRAINT [FK_Result_student] FOREIGN KEY ([student_id]) REFERENCES [student]([id_student])
)
