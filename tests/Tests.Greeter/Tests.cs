using AutoFixture;
using Google.Protobuf.Collections;
using Greeter;
using NUnit.Framework;

namespace Tests.Greeter
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void RepeatedField_without_customization_should_be_empty()
        {
            var fixture = new Fixture();

            var field = fixture.Create<RepeatedField<Language>>();

            Assert.That(field, Is.Empty);
        }

        [Test]
        public void MapField_without_customization_should_be_empty()
        {
            var fixture = new Fixture();

            var field = fixture.Create<MapField<string, string>>();

            Assert.That(field, Is.Empty);
        }

        [Test]
        public void RepeatedField_with_customization_should_not_be_empty()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(new RepeatedFieldCustomization());

            var field = fixture.Create<RepeatedField<Language>>();

            Assert.That(field, Is.Not.Empty);
        }

        [Test]
        public void MapField_with_customization_should_not_be_empty()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(new MapFieldCustomization());

            var field = fixture.Create<MapField<string, string>>();

            Assert.That(field, Is.Not.Empty);
        }

        [Test]
        public void Request_languages_with_customization_should_not_be_empty()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(new RepeatedFieldCustomization());

            var request = fixture.Create<HelloRequest>();

            Assert.That(request.Languages, Is.Not.Null);

            Assert.That(request.Languages, Is.Not.Empty);
        }

        [Test]
        public void Reply_messages_with_customization_should_not_be_empty()
        {
            var fixture = new Fixture();

            fixture.Customizations.Add(new MapFieldCustomization());

            var reply = fixture.Create<HelloReply>();

            Assert.That(reply.Messages, Is.Not.Null);

            Assert.That(reply.Messages, Is.Not.Empty);
        }
    }
}
