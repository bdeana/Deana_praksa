CREATE TABLE [dbo].[Person] (
    [OIB]       VARCHAR(20)    NOT NULL,
    [firstName] VARCHAR (20) NOT NULL,
    [lastName]  VARCHAR (20) NOT NULL,
    [mail]      VARCHAR (30) NULL,
    CONSTRAINT [oib] PRIMARY KEY CLUSTERED ([OIB] ASC)
);

