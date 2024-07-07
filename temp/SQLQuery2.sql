CREATE TABLE [dbo].[Customer] (
    [MaKhachHang] INT             NOT NULL,
    [HoVaTen]     NVARCHAR (100)  NULL,
    [DiaChi]      NVARCHAR (100)  NULL,
    [SoDienThoai] FLOAT           NULL,
    [GioiTinh]    NVARCHAR (50)   NULL,
    [NgaySinh]    DATETIME        NULL,  -- Date of birth in datetime format
    [NgayDangKi]  DATETIME        NULL,  -- Registration date in datetime format
    PRIMARY KEY CLUSTERED ([MaKhachHang] ASC)
);
