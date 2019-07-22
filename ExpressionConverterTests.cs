namespace Smbc.Gin.Common.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Expressions;
    using NUnit.Framework;

    [ TestFixture ]
    public class ExpressionConverterTests
    {
        private class Model
        {
            public Model( string value )
            {
                Value = value;
            }

            public string Value { get; }
        }

        private class Data
        {
            public Data( string value )
            {
                Value = value;
            }

            public string Value { get; }
        }

        [ Test ]
        public void Can_filter_data_list_using_string_equals_equals_model_expression()
        {
            // Arrange
            var dataList = new List<Data> { new Data( "A" ), new Data( "B" ), new Data( "C" ) };

            Expression<Func<Model, bool>> predicate = model => model.Value == "A";
            Expression<Func<Data, bool>> dataPredicate = predicate.TypeConvert<Model, Data, bool>();

            // Act
            List<Data> filtered = dataList.AsQueryable().Where( dataPredicate ).ToList();

            // Assert
            Assert.That( dataList.Count, Is.EqualTo( 3 ) );
            Assert.That( filtered.Count, Is.EqualTo( 1 ) );
            Assert.That( filtered[ 0 ].Value, Is.EqualTo( "A" ) );
        }

        [ Test ]
        public void Can_filter_data_list_using_case_insensitive_string_equals_model_expression()
        {
            // Arrange
            var dataList = new List<Data> { new Data( "A" ), new Data( "B" ), new Data( "C" ) };

            Expression<Func<Model, bool>> predicate = model => string.Equals( model.Value, "A", StringComparison.InvariantCultureIgnoreCase );
            Expression<Func<Data, bool>> dataPredicate = predicate.TypeConvert<Model, Data, bool>();

            // Act
            List<Data> filtered = dataList.AsQueryable().Where( dataPredicate ).ToList();

            // Assert
            Assert.That( dataList.Count, Is.EqualTo( 3 ) );
            Assert.That( filtered.Count, Is.EqualTo( 1 ) );
            Assert.That( filtered[ 0 ].Value, Is.EqualTo( "A" ) );
        }

        [ Test ]
        public void Can_filter_data_list_using_case_insensitive_string_current_culture_equals_model_expression()
        {
            // Arrange
            var dataList = new List<Data> { new Data( "A" ), new Data( "B" ), new Data( "C" ) };

            Expression<Func<Model, bool>> predicate = model => model.Value.Equals( "A", StringComparison.CurrentCultureIgnoreCase );
            Expression<Func<Data, bool>> dataPredicate = predicate.TypeConvert<Model, Data, bool>();

            // Act
            List<Data> filtered = dataList.AsQueryable().Where( dataPredicate ).ToList();

            // Assert
            Assert.That( dataList.Count, Is.EqualTo( 3 ) );
            Assert.That( filtered.Count, Is.EqualTo( 1 ) );
            Assert.That( filtered[ 0 ].Value, Is.EqualTo( "A" ) );
        }
        
        [ Test ]
        public void Can_filter_data_list_using_case_sensitive_string_equals_model_expression_with_same_case()
        {
            // Arrange
            var dataList = new List<Data> { new Data( "A" ), new Data( "B" ), new Data( "C" ) };

            Expression<Func<Model, bool>> predicate = model => string.Equals( model.Value, "A", StringComparison.InvariantCulture );
            Expression<Func<Data, bool>> dataPredicate = predicate.TypeConvert<Model, Data, bool>();

            // Act
            List<Data> filtered = dataList.AsQueryable().Where( dataPredicate ).ToList();

            // Assert
            Assert.That( dataList.Count, Is.EqualTo( 3 ) );
            Assert.That( filtered.Count, Is.EqualTo( 1 ) );
            Assert.That( filtered[ 0 ].Value, Is.EqualTo( "A" ) );
        }

        [ Test ]
        public void Can_filter_data_list_using_case_sensitive_string_equals_model_expression_with_different_case()
        {
            // Arrange
            var dataList = new List<Data> { new Data( "A" ), new Data( "B" ), new Data( "C" ) };

            Expression<Func<Model, bool>> predicate = model => string.Equals( model.Value, "a", StringComparison.InvariantCulture );
            Expression<Func<Data, bool>> dataPredicate = predicate.TypeConvert<Model, Data, bool>();

            // Act
            List<Data> filtered = dataList.AsQueryable().Where( dataPredicate ).ToList();

            // Assert
            Assert.That( dataList.Count, Is.EqualTo( 3 ) );
            Assert.That( filtered.Count, Is.EqualTo( 0 ) );
        }
    }
}