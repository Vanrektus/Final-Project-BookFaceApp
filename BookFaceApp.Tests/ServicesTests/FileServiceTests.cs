using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Services;
using BookFaceApp.Infrastructure.Data.Common;
using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace BookFaceApp.Tests.ServicesTests
{
    [TestFixture]
    public class FileServiceTests
    {
        private IServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();

            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IRepository, Repository>()
                .AddSingleton<IFileService, FileService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IRepository>();

            await SeedDbAsync(repo!);
        }



        [Test]
        public async Task ChangePicShouldWorkCorrectly()
        {
            var service = serviceProvider.GetService<IFileService>();
            var repo = serviceProvider.GetService<IRepository>();

            var picId = Guid.Parse("0c68c9ca-e15a-4b53-b314-493aaecf84dd");

            var picBeforeEdit = await repo!.All<ProfilePicture>()
                .FirstOrDefaultAsync(pp => pp.Id == picId);

            Assert.IsNotNull(picBeforeEdit);
            Assert.IsTrue(picBeforeEdit.FileName == "TestImage.png");

            var imageContentToString = "0x89504E470D0A1A0A0000000D4948445200000200000002000803000000C3A624C800000021504C54454C69717E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D9B2D22D90000000A74524E530017F0A5304F8A6FDBC4B08AD759000009704944415478DAEDDD0972EBD60E84E1CB79D8FF825F2537C99B6C59140F87C3FEFE05B8540204740320FDEB1700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000007FD3347D3F0CD3348DE3344DC3D0F74DE35B8988FC30754BBB7E49BB74D3200F9E1BFAB19BD73798BB511A3C2FF8CBBA8945123C867E6BF0FF9D04BD6FAF7686AE5D77D07683EFB0E6DFFEAEE8FF9503EA40A57D7F9AD742CC133D501BD3B21665997CA735857F5E8B334B816A6A7FBB1E42AB13D410FEF1A0F0FF1684522038FC52E0FEBDFFE0F0FF4E01DFF35D873EF37A0AB3E1D02DAB7FB79E46A70F4456FFFF3404BEF15BD1CFEBC9E80377A25B2F401FC8FDF9FF55046C8902BB3F25A0FC6B03CABF36105FFEB581F8F2FF4F1B1087AB58D65BB088C425343789FFBA2EA4E015F19FD7DB30CB8038F9CF0C5C1CFF76BD15AD0C3895E166F15FD7D672E8CCF8AF374406C4D67F5D40FC6540A6FFE306C55F0664CE7F4D85C55F065C40B7DE1CBBC14399D6DBE33EE04803B8560033186900580102800C2000C880C009B09930014006100064C0C10C6B55380E28CD5C5702CC2216EA0038816314605B5B02B47460AC02A403D315201D18AE00E9C0C28C6B9578A3642905B8560A1D185D009480580BC80A2A004A8002A004C417002520BC0028010598D6AAB113DA4B5B7702B422185D009480BD2CB52780870577D1AFD5E34238D6037282FB99EB4F005BE1EC0EA007ECA17B4202B80D8B1D021805EC64581F81E3C0E80EA0077CCEFA104432BA03E801C15320B3A0E83D807DC02E9AF531380B8996004440B8042002621741164224001140021001E9128008085E045807D0805460F426C83EE833FA672580BBB06813C0066C677A5602784028DA04B001DB599E950036C25B699F95004E83933701B601124002648F010C0224809826CF814C822480984A00C44E82CD822580986E627C5A02380A930090009000200221012001601208090009802F700F20012440344F4B0011DDC8FCACF87B3C782B1E0C09C7A361B64176410601C6007C201798C9C39E0DF364D8661EF578B08783C37D2017186E039880701BC00484AB401A307B1B6013103E0C3608FE8407DD85BA080D9F059A03668B0012207C12600A103E093005C85E075804841B412630BC07E800D93D400708EF013A40780FD001B27B800E103E0B32050ADF07D803ECA2FACB40D780E1329004CC96812460B80C2401F752F96DA86BD0EC69A0296078095000B255000550A404546B045A0520BB042800D9254001082F010A4076095000CA51E553629E072B48854B416BC092547817E00E205B07528085A9EC4951CF8396A6B2CB107720C5A96A2B680B78C030A0A226301B01643B010E207B1C6404942D0308806C19400084CB0002205B061000D9328000C8CE00F13F9C5B6F86ED80B3AD0003704A06DCF640CC11D84966F0A619D03280D11920FED91920FED91920FED91920FEA767C0ADDCE02CFED1F300FEFF920CB8CD4C7011FF6BB8C95EC0FCFF326EB11DB6FFBD90E17233D07A0220DA0C90FF974BC14B854047FEDD40085CD6065AEDFFCC9FFAD8DCCC0F7EEBFEBEFFA8D8F33BFF7EDC3ADDEAE7DFB76A4371B53FBF56DCFDE94560E95F3B93993B2828F5FF09EFF7DFEAC96F90F8FEFD0FC3CF2982CF75FEF4EB0E45E0456C271EE158913FFED4274EF0FE2FAAFBC8261C3DE8E92E76842F83DA19149565DCB87B69C68353A07DE9F0BA4D6A01EF69FF6DDBD76327832FBBFA37F3087EA0F890F7F502E6B02AF0FAD7FF6231450C7EF4F36F3F72617FA6C074801C9CA76673B7B2323CE0E7FFDE11CE50D8142E3F84F0A771B42250F0E7FFDE8FAAEF8A7582B6EB8FFFBCD83AD6FB595E0F2572A0ED86B33E2FDE2BA75B6E3177E6C03BD12FFA79B1E5CCEBAD415B33741F4AC2B91BDE09D9FBD3276DE01D36F9F837076DDB93E0CDE06FBD49D306CA7E9F5BE475338CCB5BBFD5761987B7FFE8D6B1931561A9F2FFD9C2A5E987B1FB2E0FDAA51B877E4BA3FE60F7A00DEC56D3057E554DDF0FC3304DE3D875E3384DC330F4FD6689F6E102DA86B04CFBFFAFD67A81BE6E3E3E41F124D13E37F5751F3839059A3D9B677EF0CB82BA6F68D39E5905F6EE9B5C0914917F57A540817523295846FE5D910285B6CD260265E4DFFF09AC838B6B5FEE938AFA01F1FFE38B3DB0BA0E453FA8B81790FF9F6D6F3FFCF117BE3462068E89FF9F32BBB41A68C6038E8C64C0AF035FF6B4941B0D34D3410F9D78B9D4B16F78E8A602BDA09F0EBC33361038FA858FEDAE24E8A7EEE8CFD78BFFF18F72759F748366EACE78D42C3B03CE7BE1EBDC8DD3BBFBBEA69FC6EEB4770F2567C0E92FFC6D976E1A5E7CE1FD3075CBE91FAA17FFB3BFF2795E96BF2F017E5F052CCB3C5FF6714233A0A67F077E707F6AC45F0688BF0C30FFCD256E2A2CFEFF9B0159F1EF443C7A3B3C8A77F48DD020DA5F117327D88BF5D7840C84EEFBBF7FAF26E37F0F1B00848F0318C06C33C800645B010620DB0AF404E04F42F0D9568000FC59089A009B093F954974A3650001902D034C80C2650001902D0308806C19600514BE16B202C86E021A407613D000C29B000790DD04EC00B39B800610DE043480EC26A0018437013B80EC9D802BC08F1929403A9002A40329403A9002A4032D81229928403A900564052BC59B000A50F38D300B986D051580F012A0006497000520BC042800D9254001082F010A4076095000C24B8002905D021A512B497D1B015B80F08D803560515A7700E1D47617E010A830959D06B9042C4E5DD7813C60B613E401C39D200F18EE0479C06C27C803863B416F043B848504240375003DC014D034D025482875DC851802848F027480EC1E600F7420356C84EC810EA4868D903170F638580708EF013C40B80FE001B27D802950F82CC81EE060EEBE0FB0093E989BEF846D820FE7DE3B612630DC081A03860F038D01B387814C60B8116402C38D2009102E02CC81B3A7C1A600E193005380F0498055F029DC77256C11700AF75D0788CD3918031905190319051903190519031905D18054200D48059A039A0532016C0013C00618041B06BB077C3EB7BC0B740C70220D13C00630016C806B905446092001248004A00168002E800BB8CB1CC020E8BC41D02D1F0DB00B388D7BEE02DC039CC64DAF42F980600F601F781EF77D30A4910167C4FFC6AF899201D9F1FF631CC40C1E6B006FFF8FA39A4E0A1C17FEAE8AFF1C378CCB2C0B4AC77E5EC6BAFE833C0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000F7E75F17BED962B5F9676D0000000049454E44AE426082";
            var imageToString = "data:image/png;base64,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";

            var picEditModel = new ProfilePicture()
            {
                FileName = "TestImageEdited.png",
                Content = Encoding.ASCII.GetBytes(imageContentToString),
                ImageToString = imageToString,
            };

            string userId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5";

            await service!.ChangePictureAsync(picEditModel, userId);

            var picAfterEdit = await repo!.GetByIdAsync<ProfilePicture>(picId);

            Assert.IsNotNull(picAfterEdit);
            Assert.IsTrue(picAfterEdit.FileName == "TestImageEdited.png");
        }



        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }

        private async Task SeedDbAsync(IRepository repo)
        {
            var imageContentToString = "0x89504E470D0A1A0A0000000D4948445200000200000002000803000000C3A624C800000021504C54454C69717E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D7E7D7D9B2D22D90000000A74524E530017F0A5304F8A6FDBC4B08AD759000009704944415478DAEDDD0972EBD60E84E1CB79D8FF825F2537C99B6C59140F87C3FEFE05B8540204740320FDEB1700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000007FD3347D3F0CD3348DE3344DC3D0F74DE35B8988FC30754BBB7E49BB74D3200F9E1BFAB19BD73798BB511A3C2FF8CBBA8945123C867E6BF0FF9D04BD6FAF7686AE5D77D07683EFB0E6DFFEAEE8FF9503EA40A57D7F9AD742CC133D501BD3B21665997CA735857F5E8B334B816A6A7FBB1E42AB13D410FEF1A0F0FF1684522038FC52E0FEBDFFE0F0FF4E01DFF35D873EF37A0AB3E1D02DAB7FB79E46A70F4456FFFF3404BEF15BD1CFEBC9E80377A25B2F401FC8FDF9FF55046C8902BB3F25A0FC6B03CABF36105FFEB581F8F2FF4F1B1087AB58D65BB088C425343789FFBA2EA4E015F19FD7DB30CB8038F9CF0C5C1CFF76BD15AD0C3895E166F15FD7D672E8CCF8AF374406C4D67F5D40FC6540A6FFE306C55F0664CE7F4D85C55F065C40B7DE1CBBC14399D6DBE33EE04803B8560033186900580102800C2000C880C009B09930014006100064C0C10C6B55380E28CD5C5702CC2216EA0038816314605B5B02B47460AC02A403D315201D18AE00E9C0C28C6B9578A3642905B8560A1D185D009480580BC80A2A004A8002A004C417002520BC0028010598D6AAB113DA4B5B7702B422185D009480BD2CB52780870577D1AFD5E34238D6037282FB99EB4F005BE1EC0EA007ECA17B4202B80D8B1D021805EC64581F81E3C0E80EA0077CCEFA104432BA03E801C15320B3A0E83D807DC02E9AF531380B8996004440B8042002621741164224001140021001E9128008085E045807D0805460F426C83EE833FA672580BBB06813C0066C677A5602784028DA04B001DB599E950036C25B699F95004E83933701B601124002648F010C0224809826CF814C822480984A00C44E82CD822580986E627C5A02380A930090009000200221012001601208090009802F700F20012440344F4B0011DDC8FCACF87B3C782B1E0C09C7A361B64176410601C6007C201798C9C39E0DF364D8661EF578B08783C37D2017186E039880701BC00484AB401A307B1B6013103E0C3608FE8407DD85BA080D9F059A03668B0012207C12600A103E093005C85E075804841B412630BC07E800D93D400708EF013A40780FD001B27B800E103E0B32050ADF07D803ECA2FACB40D780E1329004CC96812460B80C2401F752F96DA86BD0EC69A0296078095000B255000550A404546B045A0520BB042800D9254001082F010A4076095000CA51E553629E072B48854B416BC092547817E00E205B07528085A9EC4951CF8396A6B2CB107720C5A96A2B680B78C030A0A226301B01643B010E207B1C6404942D0308806C19400084CB0002205B061000D9328000C8CE00F13F9C5B6F86ED80B3AD0003704A06DCF640CC11D84966F0A619D03280D11920FED91920FED91920FED91920FEA767C0ADDCE02CFED1F300FEFF920CB8CD4C7011FF6BB8C95EC0FCFF326EB11DB6FFBD90E17233D07A0220DA0C90FF974BC14B854047FEDD40085CD6065AEDFFCC9FFAD8DCCC0F7EEBFEBEFFA8D8F33BFF7EDC3ADDEAE7DFB76A4371B53FBF56DCFDE94560E95F3B93993B2828F5FF09EFF7DFEAC96F90F8FEFD0FC3CF2982CF75FEF4EB0E45E0456C271EE158913FFED4274EF0FE2FAAFBC8261C3DE8E92E76842F83DA19149565DCB87B69C68353A07DE9F0BA4D6A01EF69FF6DDBD76327832FBBFA37F3087EA0F890F7F502E6B02AF0FAD7FF6231450C7EF4F36F3F72617FA6C074801C9CA76673B7B2323CE0E7FFDE11CE50D8142E3F84F0A771B42250F0E7FFDE8FAAEF8A7582B6EB8FFFBCD83AD6FB595E0F2572A0ED86B33E2FDE2BA75B6E3177E6C03BD12FFA79B1E5CCEBAD415B33741F4AC2B91BDE09D9FBD3276DE01D36F9F837076DDB93E0CDE06FBD49D306CA7E9F5BE475338CCB5BBFD5761987B7FFE8D6B1931561A9F2FFD9C2A5E987B1FB2E0FDAA51B877E4BA3FE60F7A00DEC56D3057E554DDF0FC3304DE3D875E3384DC330F4FD6689F6E102DA86B04CFBFFAFD67A81BE6E3E3E41F124D13E37F5751F3839059A3D9B677EF0CB82BA6F68D39E5905F6EE9B5C0914917F57A540817523295846FE5D910285B6CD260265E4DFFF09AC838B6B5FEE938AFA01F1FFE38B3DB0BA0E453FA8B81790FF9F6D6F3FFCF117BE3462068E89FF9F32BBB41A68C6038E8C64C0AF035FF6B4941B0D34D3410F9D78B9D4B16F78E8A602BDA09F0EBC33361038FA858FEDAE24E8A7EEE8CFD78BFFF18F72759F748366EACE78D42C3B03CE7BE1EBDC8DD3BBFBBEA69FC6EEB4770F2567C0E92FFC6D976E1A5E7CE1FD3075CBE91FAA17FFB3BFF2795E96BF2F017E5F052CCB3C5FF6714233A0A67F077E707F6AC45F0688BF0C30FFCD256E2A2CFEFF9B0159F1EF443C7A3B3C8A77F48DD020DA5F117327D88BF5D7840C84EEFBBF7FAF26E37F0F1B00848F0318C06C33C800645B010620DB0AF404E04F42F0D9568000FC59089A009B093F954974A3650001902D034C80C2650001902D0308806C19600514BE16B202C86E021A407613D000C29B000790DD04EC00B39B800610DE043480EC26A0018437013B80EC9D802BC08F1929403A9002A40329403A9002A4032D81229928403A900564052BC59B000A50F38D300B986D051580F012A0006497000520BC042800D9254001082F010A4076095000C24B8002905D021A512B497D1B015B80F08D803560515A7700E1D47617E010A830959D06B9042C4E5DD7813C60B613E401C39D200F18EE0479C06C27C803863B416F043B848504240375003DC014D034D025482875DC851802848F027480EC1E600F7420356C84EC810EA4868D903170F638580708EF013C40B80FE001B27D802950F82CC81EE060EEBE0FB0093E989BEF846D820FE7DE3B612630DC081A03860F038D01B387814C60B8116402C38D2009102E02CC81B3A7C1A600E193005380F0498055F029DC77256C11700AF75D0788CD3918031905190319051903190519031905D18054200D48059A039A0532016C0013C00618041B06BB077C3EB7BC0B740C70220D13C00630016C806B905446092001248004A00168002E800BB8CB1CC020E8BC41D02D1F0DB00B388D7BEE02DC039CC64DAF42F980600F601F781EF77D30A4910167C4FFC6AF899201D9F1FF631CC40C1E6B006FFF8FA39A4E0A1C17FEAE8AFF1C378CCB2C0B4AC77E5EC6BAFE833C0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000F7E75F17BED962B5F9676D0000000049454E44AE426082";
            var imageToString = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAgAAAAIACAMAAADDpiTIAAAAIVBMVEVMaXF+fX1+fX1+fX1+fX1+fX1+fX1+fX1+fX1+fX1+fX2bLSLZAAAACnRSTlMAF/ClME+Kb9vEsIrXWQAACXBJREFUeNrt3Qly69YOhOHLedj/gl8lN8mbbFkUD4fD/v4FuFQCBHQDIP3rFwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAf9M0fT8M0zSN4zRNw9D3TeNbiYj8MHVLu35Ju3TTIA+eG/qxm9c3mLtRGjwv+Mu6iUUSPIZ+a/D/nQS9b692hq5dd9B2g++w5t/+ruj/lQPqQKV9f5rXQswTPVAb07IWZZl8pzWFf16LM0uBamp/ux5CqxPUEP7xoPD/FoRSIDj8UuD+vf/g8P9OAd/zXYc+83oKs+HQLat/t55Gpw9EVv//NAS+8VvRz+vJ6AN3olsvQB/I/fn/VQRsiQK7PyWg/GsDyr82EF/+tYH48v9PGxCHq1jWW7CIxCU0N4n/ui6k4BXxn9fbMMuAOPnPDFwc/3a9Fa0MOJXhZvFf19Zy6Mz4rzdEBsTWf11A/GVApv/jBsVfBmTOf02FxV8GXEC33hy7wUOZ1tvjPuBIA7hWADMYaQBYAQKADCAAyIDACbCZMAFABhAAZMDBDGtVOA4ozVxXAswiFuoAOIFjFGBbWwK0dGCsAqQD0xUgHRiuAOnAwoxrlXijZCkFuFYKHRhdAJSAWAvICioASoACoATEFwAlILwAKAEFmNaqsRPaS1t3ArQiGF0AlIC9LLUngIcFd9Gv1eNCONYDcoL7metPAFvh7A6gB+yhe0ICuA2LHQIYBexkWB+B48DoDqAHfM76EEQyugPoAcFTILOg6D2AfcAumvUxOAuJlgBEQLgEIAJiF0EWQiQAEUACEAHpEoAICF4EWAfQgFRg9CbIPugz+mclgLuwaBPABmxnelYCeEAo2gSwAdtZnpUANsJbaZ+VAE6DkzcBtgESQAJkjwEMAiSAmCbPgUyCJICYSgDEToLNgiWAmG5ifFoCOAqTAJAAkAAgAiEBIAFgEggJAAmAL3APIAEkQDRPSwAR3cj8rPh7PHgrHgwJx6NhtkF2QQYBxgB8IBeYycOeDfNk2GYe9Xiwh4PDfSAXGG4DmIBwG8AEhKtAGjB7G2ATED4MNgj+hAfdhboIDZ8FmgNmiwASIHwSYAoQPgkwBcheB1gEhBtBJjC8B+gA2T1ABwjvATpAeA/QAbJ7gA4QPgsyBQrfB9gD7KL6y0DXgOEykATMloEkYLgMJAH3UvltqGvQ7GmgKWB4CVAAslUABVCkBFRrBFoFILsEKADZJUABCC8BCkB2CVAAylHlU2KeBytIhUtBa8CSVHgX4A4gWwdSgIWp7ElRz4OWprLLEHcgxalqK2gLeMAwoKImMBsBZDsBDiB7HGQElC0DCIBsGUAAhMsAAiBbBhAA2TKAAMjOAPE/nFtvhu2As60AA3BKBtz2QMwR2Elm8KYZ0DKA0Rkg/tkZIP7ZGSD+2Rkg/qdnwK3c4Cz+0fMA/v+SDLjNTHAR/2u4yV7A/P8ybrEdtv+9kOFyM9B6AiDaDJD/l0vBS4VAR/7dQAhc1gZa7f/Mn/rY3MwPfuv+vv+o2PM7/37cOt3q59+3akNxtT+/Vtz96UVg6V87k5k7KCj1/wnv99/qyW+Q+P79D8PPKYLPdf706w5F4EVsJx7hWJE//tQnTvD+L6r7yCYcPejpLnaEL4PaGRSVZdy4e2nGg1Ogfenwuk1qAe9p/23b12Mngy+7+jfzCH6g+JD39QLmsCrw+tf/YjFFDH70828/cmF/psB0gBycp2Zzt7IyPODn/94RzlDYFC4/hPCncbQiUPDn/96Pqu+KdYK264//vNg61vtZXg8lcqDthrM+L94rp1tuMXfmwDvRL/p5seXM661BWzN0H0rCuRveCdn70ydt4B02+fg3B23bk+DN4G+9SdMGyn6fW+R1M4zLW7/VdhmHt//o1rGTFWGp8v/ZwqXph7H7Lg/apRuHfkuj/mD3oA3sVtMFflVN3w/DME3j2HXjOE3DMPT9Zon24QLahrBM+/+v1nqBvm4+PkHxJNE+N/V1Hzg5BZo9m2d+8MuCum9o055ZBfbum1wJFJF/V6VAgXUjKVhG/l2RAoW2zSYCZeTf/wmsg4trX+6TivoB8f/jiz2wug5FP6i4F5D/n21vP/zxF740YgaOif+fMru0GmjGA46MZMCvA1/2tJQbDTTTQQ+deLnUsW946KYCvaCfDrwzNhA4+oWP7a4k6Kfu6M/Xi//xj3J1n3SDZurOeNQsOwPOe+Hr3I3Tu/u+pp/G7rR3DyVnwOkv/G2XbhpefOH9MHXL6R+qF/+zv/J5Xpa/LwF+XwUsyzxf9nFCM6Cmfwd+cH9qxF8GiL8MMP/NJW4qLP7/mwFZ8e9EPHo7PIp39I3QINpfEXMn2Iv114QMhO77v3+vJuN/DxsAhI8DGMBsM8gAZFsBBiDbCvQE4E9C8NlWgAD8WQiaAJsJP5VJdKNlAAGQLQNMgMJlAAGQLQMIgGwZYAUUvhayAshuAhpAdhPQAMKbAAeQ3QTsALObgAYQ3gQ0gOwmoAGENwE7gOydgCvAjxkpQDqQAqQDKUA6kAKkAy2BIpkoQDqQBWQFK8WbAApQ840wC5htBRWA8BKgAGSXAAUgvAQoANklQAEILwEKQHYJUADCS4ACkF0CGlErSX0bAVuA8I2ANWBRWncA4dR2F+AQqDCVnQa5BCxOXdeBPGC2E+QBw50gDxjuBHnAbCfIA4Y7QW8EO4SFBCQDdQA9wBTQNNAlSCh13IUYAoSPAnSA7B5gD3QgNWyE7IEOpIaNkDFw9jhYBwjvATxAuA/gAbJ9gClQ+CzIHuBg7r4PsAk+mJvvhG2CD+feO2EmMNwIGgOGDwONAbOHgUxguBFkAsONIAkQLgLMgbOnwaYA4ZMAU4DwSYBV8CncdyVsEXAK910HiM05GAMZBRkDGQUZAxkFGQMZBdGAVCANSAWaA5oFMgFsABPABhgEGwa7B3w+t7wLdAxwIg0TwAYwAWyAa5BURgkgASSABKABaAAugAu4yxzAIOi8QdAtHw2wCziNe+4C3AOcxk2vQvmAYA9gH3ge930wpJEBZ8T/xq+JkgHZ8f9jHMQMHmsAb/+Po5pOChwX/q6K/xw3jMssC0rHfl7Guv6DPAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA9+dfF77ZYrX5Z20AAAAASUVORK5CYII=";

            var profilePicture = new ProfilePicture()
            {
                Id = Guid.Parse("0c68c9ca-e15a-4b53-b314-493aaecf84dd"),
                FileName = "TestImage.png",
                Content = Encoding.ASCII.GetBytes(imageContentToString),
                ImageToString = imageToString,
                UserId = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5"
            };

            var user = new User()
            {
                Id = "ff8c4ff1-b3a1-4d41-8d8c-4de59272dec5",
                UserName = "Pesho",
                NormalizedUserName = "PESHO",
                FirstName = "Petar",
                LastName = "Petrov",
                Email = "pesho@mail.com",
                NormalizedEmail = "PESHO@MAIL.COM",
                ProfilePicture = new ProfilePicture()
                {
                    FileName = "",
                    Content = new byte[] { },
                    ImageToString = "",
                    UserId = ""
                },
            };

            await repo.AddAsync(user);
            await repo.AddAsync(profilePicture);
            await repo.SaveChangesAsync();
        }
    }
}
