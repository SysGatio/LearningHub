﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LearningHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class Mig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "operation-log",
                columns: table => new
                {
                    log_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    messagetext = table.Column<string>(name: "message-text", type: "text", nullable: false),
                    interfacename = table.Column<string>(name: "interface-name", type: "text", nullable: false),
                    username = table.Column<string>(name: "user-name", type: "text", nullable: false),
                    ipaddress = table.Column<string>(name: "ip-address", type: "text", nullable: false),
                    occurrencedate = table.Column<DateTime>(name: "occurrence-date", type: "timestamp without time zone", nullable: false),
                    recordtype = table.Column<int>(name: "record-type", type: "integer", nullable: false),
                    exceptiontext = table.Column<string>(name: "exception-text", type: "text", nullable: true),
                    exceptionstacktrace = table.Column<string>(name: "exception-stack-trace", type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operation-log", x => x.log_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "operation-log");
        }
    }
}
