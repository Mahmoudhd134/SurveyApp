using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleAdminAndAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
             * Admin Account Has 'Admin' Role 
             * username => Mahmoudhd134
             * email => mahmoudnasser123@gmail.com
             * password => Mahmoud2320030@
             */
            migrationBuilder.Sql(@"
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a10d30e1-15ab-446d-8ecb-6c1b90f16958', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'474a9fc5-2fc3-4188-aa27-cec2a3aa7681', N'Mahmoud', N'Nasser', N'Mahmoudhd134', N'MAHMOUDHD134', N'mahmoudnasser123@gmail.com', N'MAHMOUDNASSER123@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEH8k8PPuvbxb0se51pulXRPGz1wKswLXqNSJQbAmAen7PeeTAdyhk7xUJLTXEMuV3g==', N'JCBZ5KHE6OKLHULEIJ7ZMKCTAWWWN2NT', N'811b68dc-f1a7-4649-b835-56273c0ab78c', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'474a9fc5-2fc3-4188-aa27-cec2a3aa7681', N'a10d30e1-15ab-446d-8ecb-6c1b90f16958')
GO
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
