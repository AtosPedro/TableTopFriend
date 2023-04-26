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

                    b.Property<byte[]>("AudioClip")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("AudioLink")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<byte[]>("ProfileImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserRole")
                        .HasColumnType("int")
                        .HasComment("0 - Admin, 1 - Free user, 2 - Premium user");

                    b.Property<int>("Validation")
                        .HasColumnType("int")
                        .HasComment("0 - Not validated, 1 - Validated");

                    b.Property<DateTime?>("ValidationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

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

                    b.OwnsOne("DDDTableTopFriend.Domain.AggregateCharacter.Entities.CharacterSheet", "CharacterSheet", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

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

                            b1.Navigation("SkillIds");

                            b1.Navigation("StatusIds");
                        });

                    b.Navigation("AudioEffectIds");

                    b.Navigation("CharacterSheet")
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

                    b.Navigation("AudioEffectIds");

                    b.Navigation("CharacterIds");
                });
#pragma warning restore 612, 618
        }
    }
}
