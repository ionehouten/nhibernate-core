using System;

using NHibernate.Engine;
using NHibernate.Util;
using NExpression = NHibernate.Expression;
using NHibernate.SqlCommand;

using NHibernate.DomainModel;
using NUnit.Framework;

namespace NHibernate.Test.ExpressionTest
{
	/// <summary>
	/// Summary description for InsensitiveLikeExpressionFixture.
	/// </summary>
	[TestFixture]
	public class InsensitiveLikeExpressionFixture : BaseExpressionFixture
	{
		[Test]
		public void InsentitiveLikeSqlStringTest() 
		{
			ISession session = factory.OpenSession();
			
			NExpression.ICriterion expression = NExpression.Expression.InsensitiveLike("Address", "12 Adress");

			CreateObjects( typeof( Simple ), session );
			SqlString sqlString = expression.ToSqlString(criteria, criteriaQuery, CollectionHelper.EmptyMap);
			
			string expectedSql = "lower(sql_alias.address) like ?";
			if ((factory as ISessionFactoryImplementor).Dialect is Dialect.PostgreSQLDialect)
			{
				expectedSql = "sql_alias.address ilike ?";
			}

			CompareSqlStrings(sqlString, expectedSql, 1);

			session.Close();
		}


	}
}
