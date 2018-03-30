using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentMigrator;

namespace Tourine.Migration
{
    [Migration(00220180330)]
    public class EnableCascadeDeleteOnTourDependecies : Common.MigrationBase
    {
    }
}