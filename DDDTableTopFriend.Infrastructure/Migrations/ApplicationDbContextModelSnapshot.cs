﻿// <auto-generated />
using System;
using DDDTableTopFriend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DDDTableTopFriend.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateAudioEffect.AudioEffect", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AudioEffects", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateCampaign.Campaign", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Campaigns", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateCharacter.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasComment("0 - Player, 1 - NPC, 2 - Enemy");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Characters", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateSession.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CampaignId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Sessions", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateSkill.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AudioEffectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Skills", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateStatus.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateUser.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("ProfileImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.Common.Models.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Error")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessage", (string)null);
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateAudioEffect.AudioEffect", b =>
                {
                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("AudioEffectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(5000)
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("AudioEffectId");

                            b1.ToTable("AudioEffects");

                            b1.WithOwner()
                                .HasForeignKey("AudioEffectId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("AudioEffectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("AudioEffectId");

                            b1.ToTable("AudioEffects");

                            b1.WithOwner()
                                .HasForeignKey("AudioEffectId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects.AudioClip", "Clip", b1 =>
                        {
                            b1.Property<Guid>("AudioEffectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<byte[]>("Value")
                                .IsRequired()
                                .HasColumnType("varbinary(max)")
                                .HasColumnName("AudioClip");

                            b1.HasKey("AudioEffectId");

                            b1.ToTable("AudioEffects");

                            b1.WithOwner()
                                .HasForeignKey("AudioEffectId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects.YoutubeVideoUrl", "AudioLink", b1 =>
                        {
                            b1.Property<Guid>("AudioEffectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(600)
                                .HasColumnType("nvarchar(600)")
                                .HasColumnName("AudioLink");

                            b1.HasKey("AudioEffectId");

                            b1.ToTable("AudioEffects");

                            b1.WithOwner()
                                .HasForeignKey("AudioEffectId");
                        });

                    b.Navigation("AudioLink");

                    b.Navigation("Clip");

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateCampaign.Campaign", b =>
                {
                    b.OwnsMany("DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects.CharacterId", "CharacterIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CampaignId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("CharacterId");

                            b1.HasKey("Id");

                            b1.HasIndex("CampaignId");

                            b1.ToTable("CampaignCharacterIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CampaignId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("CampaignId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(5000)
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("CampaignId");

                            b1.ToTable("Campaigns");

                            b1.WithOwner()
                                .HasForeignKey("CampaignId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("CampaignId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("CampaignId");

                            b1.ToTable("Campaigns");

                            b1.WithOwner()
                                .HasForeignKey("CampaignId");
                        });

                    b.OwnsMany("DDDTableTopFriend.Domain.AggregateSession.ValueObjects.SessionId", "SessionIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CampaignId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("SessionId");

                            b1.HasKey("Id");

                            b1.HasIndex("CampaignId");

                            b1.ToTable("CampaignSessionIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CampaignId");
                        });

                    b.Navigation("CharacterIds");

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("SessionIds");
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateCharacter.Character", b =>
                {
                    b.OwnsMany("DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects.AudioEffectId", "AudioEffectIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AudioEffectId");

                            b1.HasKey("Id");

                            b1.HasIndex("CharacterId");

                            b1.ToTable("CharacterAudioEffectIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(5000)
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Characters");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.AggregateCharacter.Entities.CharacterSheet", "CharacterSheet", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime?>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("Id", "CharacterId");

                            b1.HasIndex("CharacterId")
                                .IsUnique();

                            b1.ToTable("CharacterSheets", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");

                            b1.OwnsMany("DDDTableTopFriend.Domain.AggregateSkill.ValueObjects.SkillId", "SkillIds", b2 =>
                                {
                                    b2.Property<Guid>("CharacterSheetId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("CharacterId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("Value")
                                        .HasColumnType("uniqueidentifier")
                                        .HasColumnName("SkillId");

                                    b2.HasKey("CharacterSheetId", "CharacterId");

                                    b2.ToTable("CharacterSheetSkillIds", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("CharacterSheetId", "CharacterId");
                                });

                            b1.OwnsMany("DDDTableTopFriend.Domain.AggregateStatus.ValueObjects.StatusId", "StatusIds", b2 =>
                                {
                                    b2.Property<Guid>("CharacterSheetId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("CharacterId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("Value")
                                        .HasColumnType("uniqueidentifier")
                                        .HasColumnName("StatusId");

                                    b2.HasKey("CharacterSheetId", "CharacterId");

                                    b2.ToTable("CharacterSheetStatusIds", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("CharacterSheetId", "CharacterId");
                                });

                            b1.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Description", "Description", b2 =>
                                {
                                    b2.Property<Guid>("CharacterSheetId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("CharacterSheetCharacterId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(5000)
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("Description");

                                    b2.HasKey("CharacterSheetId", "CharacterSheetCharacterId");

                                    b2.ToTable("CharacterSheets");

                                    b2.WithOwner()
                                        .HasForeignKey("CharacterSheetId", "CharacterSheetCharacterId");
                                });

                            b1.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Name", "Name", b2 =>
                                {
                                    b2.Property<Guid>("CharacterSheetId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("CharacterSheetCharacterId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)")
                                        .HasColumnName("Name");

                                    b2.HasKey("CharacterSheetId", "CharacterSheetCharacterId");

                                    b2.ToTable("CharacterSheets");

                                    b2.WithOwner()
                                        .HasForeignKey("CharacterSheetId", "CharacterSheetCharacterId");
                                });

                            b1.Navigation("Description")
                                .IsRequired();

                            b1.Navigation("Name")
                                .IsRequired();

                            b1.Navigation("SkillIds");

                            b1.Navigation("StatusIds");
                        });

                    b.Navigation("AudioEffectIds");

                    b.Navigation("CharacterSheet")
                        .IsRequired();

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateSession.Session", b =>
                {
                    b.OwnsMany("DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects.AudioEffectId", "AudioEffectIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("AudioEffectId");

                            b1.HasKey("Id");

                            b1.HasIndex("SessionId");

                            b1.ToTable("SessionAudioEffectIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.OwnsMany("DDDTableTopFriend.Domain.AggregateCharacter.ValueObjects.CharacterId", "CharacterIds", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("CharacterId");

                            b1.HasKey("Id");

                            b1.HasIndex("SessionId");

                            b1.ToTable("SessionCharacterIds", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(5000)
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("SessionId");

                            b1.ToTable("Sessions");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("SessionId");

                            b1.ToTable("Sessions");

                            b1.WithOwner()
                                .HasForeignKey("SessionId");
                        });

                    b.Navigation("AudioEffectIds");

                    b.Navigation("CharacterIds");

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateSkill.Skill", b =>
                {
                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("SkillId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(5000)
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("SkillId");

                            b1.ToTable("Skills");

                            b1.WithOwner()
                                .HasForeignKey("SkillId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("SkillId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("SkillId");

                            b1.ToTable("Skills");

                            b1.WithOwner()
                                .HasForeignKey("SkillId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateStatus.Status", b =>
                {
                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("StatusId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(5000)
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("StatusId");

                            b1.ToTable("Status");

                            b1.WithOwner()
                                .HasForeignKey("StatusId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.Common.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("StatusId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("Name");

                            b1.HasKey("StatusId");

                            b1.ToTable("Status");

                            b1.WithOwner()
                                .HasForeignKey("StatusId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("DDDTableTopFriend.Domain.AggregateUser.User", b =>
                {
                    b.OwnsOne("DDDTableTopFriend.Domain.AggregateUser.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.AggregateUser.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Salt")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("PasswordSalt");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Password");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("DDDTableTopFriend.Domain.AggregateUser.ValueObjects.Validation", "Validation", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("ValidationDate")
                                .HasColumnType("datetime2")
                                .HasColumnName("ValidationDate");

                            b1.Property<int>("Value")
                                .HasColumnType("int")
                                .HasColumnName("Validation")
                                .HasComment("0 - Not Validated, 1 - Validated");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("Validation")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
