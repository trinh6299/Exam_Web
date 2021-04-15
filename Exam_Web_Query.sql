use master 
go

if EXISTS (select name from master.sys.databases where name = 'Exam_Web')
	drop database Exam_Web
go

create database Exam_Web
go
use Exam_Web
go

--CREATE TABLE TaiKhoan
create table TaiKhoan
(
	TaiKhoanID int primary key identity(1,1),
	UserName varchar(50),
	[Password] varchar(50),
	[Role] varchar(10),
)

-- insert into TaiKhoan
insert into TaiKhoan values('admin','admin','admin')--1
insert into TaiKhoan values('admin1','admin1','admin')--2
insert into TaiKhoan values('admin2','admin2','admin')--3
insert into TaiKhoan values('gv1','gv1','teacher')--4
insert into TaiKhoan values('gv2','gv2','teacher')--5 
insert into TaiKhoan values('quynhdtn','quynhdtn','student')--6 
insert into TaiKhoan values('anhnv','anhnv','student')--7 

--CREATE TABLE MonHoc
create table MonHoc
(
	MonHocID int primary key identity(1,1),
	TenMH nvarchar(50),
)
--insert into MonHoc
insert into MonHoc values (N'Toán học')--1
insert into MonHoc values (N'Vật lý')--2
insert into MonHoc values (N'Hóa học')--3
insert into MonHoc values (N'Ngữ văn')--4
insert into MonHoc values (N'Tiếng anh')--5
insert into MonHoc values (N'Sinh học')--6
insert into MonHoc values (N'Lịch sử')--7
insert into MonHoc values (N'Địa lý')--8
insert into MonHoc values (N'GDCD')--9
--CREATE TABLE HocVi
create table HocVi
(
	HocViID int primary key identity(1,1),
	TenHocVi nvarchar(50),
)
---insert into HocVi
insert into HocVi values(N'Giáo sư')
insert into HocVi values(N'Tiến Sĩ')
insert into HocVi values(N'Thạc sĩ')
insert into HocVi values(N'Giáo viên bộ môn')
--CREATE TABLE GiaoVien
create table GiaoVien
(
	GiaoVienID int primary key identity(1,1),
	TaiKhoanID int,
	TenGV nvarchar(50),
	NgaySinh date,
	GioiTinh bit, --1 is man, 0 is woman
	Email varchar(50),
	MonHocID int,
	HocViID int,
	constraint FK_GiaoVien_TaiKhoan foreign key (TaiKhoanID) references TaiKhoan(TaiKhoanID) on delete cascade,
	constraint FK_GiaoVien_HocVi foreign key (HocViID) references HocVi(HocViID) on delete cascade,
	constraint FK_GiaoVien_MonHoc foreign key (MonHocID) references MonHoc(MonHocID) on delete cascade,
)
--insert GiaoVien
insert into GiaoVien values('4','Giao Vien 1','1980/1/1',1,'giaovien1@gmail.com',1,1)--1
insert into GiaoVien values('5','Giao Vien 2','1983/1/23',0,'giaovien2@gmail.com',1,2)--2

--CREATE TABLE HocSinh
create table HocSinh
(
	HocSinhID int primary key identity(1,1),
	TaiKhoanID int,
	TenHS nvarchar(50),
	NgaySinh date,
	GioiTinh bit, --1 is man, 0 is woman
	Email varchar(50),
	constraint FK_HocSinh_TaiKhoan foreign key (TaiKhoanID) references TaiKhoan(TaiKhoanID) on delete cascade,
)
--insert HocSinh
insert into HocSinh values(6,N'Đào Thị Ngọc Quỳnh','1999/02/06',1,'quynhdtn@gmail.com')--1
insert into HocSinh values(7,N'Nguyễn Văn Anh','1998/08/08',1,'anhnv@gmail.com')--2

--CREATE TABLE PhanHoi
create table PhanHoi
(
	PhanHoiID int primary key identity(1,1),
	NoiDung nvarchar(500),
	HocSinhID int,
	TrangThai nvarchar(20),
	constraint FK_PhanHoi_HocSinh foreign key (HocSinhID) references HocSinh(HocSinhID) on delete cascade,
)
--insert PhanHoi
insert into PhanHoi values('Day la Phan hoi',1,'Da tra loi phan hoi')
insert into PhanHoi values('Phan hoi ngay hom nay',2,'Da tra loi phan hoi')

--CRAETE TABLE DoKho
create table DoKho
(
	DoKhoID int primary key identity(1,1),
	TenDoKho nvarchar(20),
)
--insert DoKho
insert into DoKho values(N'Dễ')
insert into DoKho values(N'Trung bình')
insert into DoKho values(N'Khó')
insert into DoKho values(N'Rất khó')

--CREATE TABLE CauHoi
create table CauHoi
(
	CauHoiID int primary key identity(1,1),
	NoiDungCauHoi nvarchar(500),
	Answer_A nvarchar(200),
	Answer_B nvarchar(200),
	Answer_C nvarchar(200),
	Answer_D nvarchar(200),
	CauTraLoiDung varchar(1),
	DoKhoID int,
--DoKho values (1)--Dễ
--DoKho values (2)--Trung bình
--DoKho values (3)--Khó
--DoKho values (4)--Rất khó
	MonHocID int,
	constraint FK_CauHoi_MonHoc foreign key (MonHocID) references MonHoc(MonHocID) on delete cascade,
	constraint FK_CauHoi_DoKho foreign key (DoKhoID) references DoKho(DoKhoID) on delete cascade,
)
--insert into CauHoi
insert into CauHoi values(N'Hội nghị Ianta (1945) có sự tham gia của các nước nào?',N'Anh - Pháp - Mĩ',N'Anh - Mĩ - Liên Xô.',N' Anh - Pháp - Đức.',N'Mĩ - Liên Xô - Trung Quốc.','B',2,7)
insert into CauHoi values(N'Hội nghị Ianta được triệu tập vào thời điểm nào của cuộc Chiến tranh thế giới thứ hai (1939 – 1945)?',N'Chiến tranh thế giới thứ hai bùng nổ',N'Chiến tranh thế giới thứ hai bước vào giai đoạn ác liệt',N'Chiến tranh thế giới thứ hai bước vào giai đoạn kết thúc.',N'Chiến tranh thế giới thứ hai đã kết thúc','C',2,7)
insert into CauHoi values(N'Mark the letter A, B, C, or Don your answer sheet to indicate the word whose underlined part differs from the other three in pronunciation in each of the following',N'ha<u>l</u>f',N'calm',N'chalk',N'culture','A',1,5)
insert into CauHoi values(N'Kim loại nào sau đây không tác dụng với dung dịch CuSO4? ',N'Ag',N'Mg',N'Fe',N'Al','A',1,3)
insert into CauHoi values(N'Kim loại nào sau đây là kim loại kiềm? ',N'Cu',N'Na',N'Mg',N'Al','B',1,3)
insert into CauHoi values(N'Khí X sinh ra trong quá trình đốt nhiên liệu hóa thạch, rất độc và gây ô nhiễm môi trường.Khí X là',N'CO',N'H2',N'NH3',N'N2','A',1,3)
insert into CauHoi values(N'Thủy phân este CH3CH2COOCH3, thu được ancol có công thức là',N'CH3OH',N' C3H7OH',N' C2H5OH',N'C3H5OH','A',1,3)
insert into CauHoi values(N'Ở nhiệt độ thường, kim loại Fe không phản ứng với dung dịch nào sau đây?',N'NaNO3',N'HCl.',N'CuSO4.',N'CuSO4.','A',1,3)
insert into CauHoi values(N'Dung dịch chất nào sau đây làm xanh quỳ tím?',N'Metanol',N'Metanol',N'Metanol',N'Metylamin','D',1,3)
insert into CauHoi values(N'Chất nào sau đây có tính lưỡng tính?',N'NaNO3',N'MgCl2',N'Al(OH)3',N' Na2CO3','C',1,3)
insert into CauHoi values(N'Sắt có số oxi hóa +3 trong hợp chất nào sau đây? ',N'. Fe(OH)2',N'Fe(NO3)2.',N' Fe2(SO4)3. ',N'FeO.','',1,3)
insert into CauHoi values(N'Chất nào sau đây có phản ứng trùng hợp?',N'Etilen',N'Etylen glicol',N'Etylamin.',N'Axit axetic.','A',1,3)
insert into CauHoi values(N'Số nguyên tử cacbon trong phân tử glucozơ là',N'5',N'10',N'6',N'12','C',1,3)
insert into CauHoi values(N'Ở nhiệt độ thường, kim loại nào sau đây tan hết trong nước dư?',N'Ba',N'Al',N'Fe',N'Fe','A',1,3)
insert into CauHoi values(N'Chất nào sau đây được dùng để làm mềm nước có tính cứng tạm thời?',N'. CaCO3.',N'MgCl2',N'NaOH',N'Fe(OH)2','C',1,3)
insert into CauHoi values(N'Dung dịch KOH tác dụng với chất nào sau đây tạo ra kết tủa Fe(OH)3? ',N'FeCl3.',N'FeO',N'Fe2O3',N'Fe3O4','A',1,3)
insert into CauHoi values(N'Fe3O4',N'HCl',N'KNO3',N' CH3COOH',N' CH3COOH','C',1,3)
insert into CauHoi values(N'Thủy phân triolein có công thức (C17H33COO)3C3H5 trong dung dịch NaOH, thu được glixerol và muối X. Công thức của X là',N'C17H35COONa',N'CH3COONa',N'CH3COONa',N'C17H33COONa','D',1,3)
insert into CauHoi values(N'Natri hiđroxit (còn gọi là xút ăn da) có công thức hóa học là',N'NaOH',N'NaHCO3',N'Na2CO3',N'Na2SO4','A',1,3)
insert into CauHoi values(N'Chất nào sau đây có một liên kết ba trong phân tử?',N'Metan',N'Etilen',N'Axetilen',N'Benzen','C',1,3)
insert into CauHoi values(N'Chất X có công thức H2N-CH(CH3)-COOH. Tên gọi của X là',N'. glyxin',N'valin',N'alanin',N'lysin','C',1,3)
insert into CauHoi values(N'Thành phần chính của vỏ các loài ốc, sò, hến là',N'Ca(NO3)2',N'CaCO3',N'NaCl',N'Na2CO3','B',1,3)
insert into CauHoi values(N'Cho m gam bột Zn tác dụng hoàn toàn với dung dịch CuSO4 dư, thu được 9,6 gam kim loại Cu. Giá trị của m là',N'6,50',N'3,25',N'9,75',N'13,00','C',1,3)
insert into CauHoi values(N'Hòa tan hoàn toàn 0,1 mol Al bằng dung dịch NaOH dư, thu được V lít H2. Giá trị của V là',N'2,24',N'5,60',N'4,48',N'3,36','D',1,3)
insert into CauHoi values(N'Cho 2 ml ancol etylic vào ống nghiệm đã có sẵn vài viên đá bọt. Thêm từ từ 4 ml dung dịch H2SO4 đặc vào ống nghiệm, đồng thời lắc đều rồi đun nóng hỗn hợp. Hiđrocacbon sinh ra trong thí nghiệm trên là',N' etilen',N'axetilen.',N'propilen',N'metan','A',1,3)
insert into CauHoi values(N'Phát biểu nào sau đây sai?',N'Dung dịch lysin không làm đổi màu quỳ tím',N'Metylamin là chất khí tan nhiều trong nước',N' Protein đơn giản chứa các gốc a-amino axit.',N'Phân tử Gly-Ala-Val có ba nguyên tử nitơ','A',1,3)
insert into CauHoi values(N'Thủy phân 68,4 gam saccarozơ với hiệu suất 75%, thu được m gam glucozơ. Giá trị của m là',N'54',N' 27',N'72.',N'36','B',1,3)
insert into CauHoi values(N'Cho m gam Gly-Ala tác dụng hết với dung dịch NaOH dư, đun nóng. Số mol NaOH đã phản ứng là 0,2 mol. Giá trị của m là ',N' 14,6',N'29,2',N'26,4.',N'32,8.','A',1,3)
insert into CauHoi values(N'Chất X được tạo thành trong cây xanh nhờ quá trình quang hợp. Ở điều kiện thường, X là chất rắn vô định hình. Thủy phân X nhờ xúc tác axit hoặc enzim, thu được chất Y có ứng dụng làm thuốc tăng lực trong y học. Chất X và Y lần lượt là',N'tinh bột và glucozơ.',N'tinh bột và saccarozơ',N'xenlulozơ và saccarozơ',N'saccarozơ và glucozơ','A',1,3)
insert into CauHoi values(N'Phát biểu nào sau đây sai?',N'Cho viên kẽm vào dung dịch HCl thì kẽm bị ăn mòn hóa học. ',N'Quặng boxit là nguyên liệu dùng để sản xuất nhôm.',N'Đốt Fe trong khí Cl2 dư, thu được FeCl3.',N'Tính khử của Ag mạnh hơn tính khử của Cu','D',1,3)
insert into CauHoi values(N'Hỗn hợp FeO và Fe2O3 tác dụng với lượng dư dung dịch nào sau đây không thu được muối sắt (II)?',N'HNO3 đặc, nóng',N'HCl',N' H2SO4 loãng.',N'. NaHSO4.','A',1,3)
insert into CauHoi values(N'Cho các tơ sau: visco, capron, xenlulozơ axetat, olon. Số tơ tổng hợp là',N'1',N'2',N'3','4',N'B',1,3)
insert into CauHoi values(N'The instructor blew his whistle and...',N'off the runners were running ',N'off ran the runners',N'off were running the runners',N'the runners runs off','B',1,5)
insert into CauHoi values(N'People have used coal and oil to______electricity for a long time',N'bred',N'raise',N'cultivate',N'generate','D',1,5)
insert into CauHoi values(N'In the early years of the 20th century, several rebellions______in the northern parts of the country.',N'turned out',N'rose up',N' broke out',N'came up','C',1,5)
insert into CauHoi values(N'The festival has many attractions. It will include contemporary orchestra music and an opera.____ , there will be poetry readings and theatrical presentations.',N'Otherwise',N'Furthermore',N'Nevertheless',N'On the other hand','B',1,5)
insert into CauHoi values(N'When he started that company, he really went______. It might have been a disaster.',N'out on the limb',N'on and off C',N'over the odds',N'.once too often','D',1,5)
insert into CauHoi values(N'We regret to tell you that the materials you ordered are______.',N'out of stock',N'out of practice',N'out of reach',N'out of work','A',1,5)
insert into CauHoi values(N'My sister is a woman of______age.',N'marriage',N'married',N'marrying',N'marriageable','D',1,5)
insert into CauHoi values(N'The fire______to have started in the furnace under the house',N'is believed',N'that is believed',N' they believed',N'that they believe','A',1,5)
insert into CauHoi values(N'This is the latest news from earthquake site. Two- thirds of the city______in a fire.',N'has been destroyed',N'have been destroyed',N'were destroyed',N'was destroyed','A',1,5)
insert into CauHoi values(N'There are many______in our library.',N'interesting American old history books',N'old American interesting history books',N'interesting old American history books',N' American interesting old history books','A',1,5)
insert into CauHoi values(N'Giả sử một gen được cấu tạo từ 3 loại nuclêôtit: A, T, G thì trên mạch gốc của gen này có thể có tối đa bao nhiêuloại mã bộ ba?',N'6 loại mã bộ ba',N'3 loại mã bộ ba',N'27 loại mã bộ ba',N'9 loại mã bộ ba','C',1,6)
insert into CauHoi values(N'Ở sinh vật nhân thực, trình tự nuclêôtit trong vùng mã hóa của gen nhưng không mã hóa axit amin được gọi là',N'đoạn intron.',N'đoạn êxôn',N' gen phân mảnh.',N'vùng vận hành','A',1,6)
insert into CauHoi values(N'Vùng điều hoà là vùng',N'quy định trình tự sắp xếp các axit amin trong phân tử prôtêin',N'mang tín hiệu khởi động và kiểm soát quá trình phiên mã',N'mang thông tin mã hoá các axit amin',N'mang tín hiệu kết thúc phiên mã','B',1,6)
insert into CauHoi values(N'Trong 64 bộ ba mã di truyền, có 3 bộ ba không mã hoá cho axit amin nào. Các bộ ba đó là:',N'UGU, UAA, UAG',N'UUG, UGA, UAG',N'UAG, UAA, UGA',N'UUG, UAA, UGA','C',1,6)
insert into CauHoi values(N'Trong quá trình nhân đôi ADN, vì sao trên mỗi chạc tái bản có một mạch được tổng hợp liên tục còn mạch kiađược tổng hợp gián đoạn?',N'Vì enzim ADN polimeraza chỉ tổng hợp mạch mới theo chiều 5’→3’.',N'Vì enzim ADN polimeraza chỉ tác dụng lên một mạch',N'Vì enzim ADN polimeraza chỉ tác dụng lên mạch khuôn 3’→5’',N'Vì enzim ADN polimeraza chỉ tác dụng lên mạch khuôn 5’→3’.','A',2,6)
insert into CauHoi values(N'Mã di truyền có tính đặc hiệu, tức là',N'. tất cả các loài đều dùng chung một bộ mã di truyền.',N'mã mở đầu là AUG, mã kết thúc là UAA, UAG, UGA',N'nhiều bộ ba cùng xác định một axit amin.',N'một bộ ba mã hoá chỉ mã hoá cho một loại axit amin.','D',1,6)
insert into CauHoi values(N'Tất cả các loài sinh vật đều có chung một bộ mã di truyền, trừ một vài ngoại lệ, điều này biểu hiện đặc điểm gìcủa mã di truyền?',N'Mã di truyền có tính đặc hiệu',N'Mã di truyền có tính thoái hóa.',N'Mã di truyền có tính phổ biến',N'. Mã di truyền luôn là mã bộ ba','C',1,6)
insert into CauHoi values(N'Gen không phân mảnh có',N'cả exôn và intrôn',N'vùng mã hoá không liên tục',N'vùng mã hoá liên tục',N'các đoạn intrôn','C',2,6)
insert into CauHoi values(N'Một đoạn của phân tử ADN mang thông tin mã hoá cho một chuỗi pôlipeptit hay một phân tử ARN được gọi là',N'codon',N'gen.',N'anticodon',N'. mã di truyền','B',1,6)
insert into CauHoi values(N' Quá trình nhân đôi ADN được thực hiện theo nguyên tắc gì?',N'Hai mạch được tổng hợp theo nguyên tắc bổ sung song song liên tục.',N'Một mạch được tổng hợp gián đoạn',N'một mạch được tổng hợp liên tục',N'Nguyên tắc bổ sung và nguyên tắc bán bảo toàn.','C',2,6)
--insert into CauHoi values(N'',N'',N'',N'',N'','',,)

--CREATE TABLE DeThi
create table DeThi
(
	DeThiID int primary key identity(1,1),
	MonHocID int,
	TenDeThi nvarchar(200),
	ThoiGianLamBai int,
	ThoiGianBatDauLamBai datetime,
	LoaiDe varchar(20),
	--revision: lam nhieu lan, lam luc nao cũng dk
	--test: chi duoc lam 1 lan
	--final test: chi duoc lam 1 lan vao dung thoi gian quy dinh
	GiaoVienID int,
	constraint FK_DeThi_GiaoVien foreign key (GiaoVienID) references GiaoVien(GiaoVienID) on delete cascade,
	constraint FK_DeThi_MonHocID foreign key (MonHocID) references MonHoc(MonHocID),
)
--insert into DeThi
insert into deThi values(3,N'Kiểm tra 15p',3600,'03/02/2021','test',1)--1

--CREATE TABLE LanThi
create table LanThi
(
	LanThiID int primary key identity(1,1),
	HocSinhID int,
	DeThiID int,
	LanThiSo int,
	KetQuaThi float,
	ThoiGianLamBai int,
	--ThoiGianLamBai luu la second
	ThoiGianNopBai datetime,
	constraint FK_LanThi_HocSinh foreign key (HocSinhID) references HocSinh(HocSinhID) on delete cascade,
	constraint FK_LanThi_DeThi foreign key (DeThiID) references DeThi(DeThiID),
)
--insert into LanThi
insert into LanThi values(1,1,1,7,200,'02/23/2021');

--CREATE TABLE KetQuaThi
--create table KetQuaThi
create table DapAnDaLuaChon
(
	KetQuaThiID int primary key identity(1,1),
	LanThiID int,
	CauHoiID int,
	DapAnDaChon varchar(1),
	constraint FK_DapAnDaLuaChon_LanThi foreign key (LanThiID) references LanThi(LanThiID) on delete cascade,
	constraint FK_DapAnDaLuaChon_CauHoi foreign key (CauHoiID) references CauHoi(CauHoiID) on delete cascade,
)
--insert into KetQua
insert into DapAnDaLuaChon values(1,1,'A');
--CREATE TABLE DeThi_CauHoi
create table DeThi_CauHoi
(
	DeThiID int,
	CauHoiID int,
	constraint PK_DeThi_CauHoi primary key (DeThiID,CauHoiID),
	constraint PK_DeThi_CauHoi_DeThi foreign key (DeThiID) references DeThi(DeThiID) on delete cascade,
	constraint PK_DeThi_CauHoi_CauHoi foreign key (CauHoiID) references CauHoi(CauHoiID),
)
--insert into DeThi_CauHoi
insert into DeThi_CauHoi values(1,4);
insert into DeThi_CauHoi values(1,5);
insert into DeThi_CauHoi values(1,6);
insert into DeThi_CauHoi values(1,7);
insert into DeThi_CauHoi values(1,8);
insert into DeThi_CauHoi values(1,9);
insert into DeThi_CauHoi values(1,10);
insert into DeThi_CauHoi values(1,11);
insert into DeThi_CauHoi values(1,12);
insert into DeThi_CauHoi values(1,13);
insert into DeThi_CauHoi values(1,14);

--CREATE TABLE LienHe
create table LienHe(
	LienHeID int primary key identity(1,1),
	HoVaTen nvarchar(50),
	Email varchar(50),
	PhanHoi nvarchar(max),
	NgayGui datetime,
)

--insert into LienHe
--date format MM/dd/yyyy
insert into LienHe values(N'Nguyễn Thu Trang','trangnt@gmail.com',N'Nguyễn Thu Trang phản Hồi','02/14/2021')
insert into LienHe values(N'Nguyễn Thanh Hùng','trangnt@gmail.com',N'Nguyễn Thanh Hùng phản Hồi','02/20/2021')


