using Newtonsoft.Json;

namespace BallPOoN{
	[JsonObject]
	public class Person {
		[JsonProperty("name")]
		public string Name { get; private set; }

		[JsonProperty("token")]
		public string Token { get; private set; }

		[JsonProperty("tokenSec")]
		public string TokenSec { get; private set; }

		[JsonProperty("created")]
		public string Created { get; private set; }

		public Person(string _name, string _token, string _tokenSec, string _created) {
			this.Name = _name;
			this.Token = _token;
			this.TokenSec = _tokenSec;
			this.Created = _created;
		}
	}

	[JsonObject]
	public class Post {
		[JsonProperty("name")]
		public string Name { get; private set; }

		[JsonProperty("time")]
		public string Time { get; private set; }

		[JsonProperty("longtitude")]
		public string Longtitude { get; private set; }

		[JsonProperty("latitude")]
		public string Latitude { get; private set; }

		[JsonProperty("feel")]
		public string Feel { get; private set; }

		[JsonProperty("comment")]
		public string Comment { get; private set; }

		public Post(string _name, string _time, string _longtitude, string _latitude, string _feel, string _comment) {
			Name = _name;
			Time = _time;
			Longtitude = _longtitude;
			Latitude = _latitude;
			Feel = _feel;
			Comment = _comment;
		}
	};

	[JsonObject]
	public class Key {
		[JsonProperty("name")]
		public string Name { get; private set; }

		[JsonProperty("since")]
		public string Since { get; private set; }

		[JsonProperty("to")]
		public string To { get; private set; }

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("count")]
		public byte Count { get; private set; }

		public Key(string _name, string _since, string _to, ulong _id, byte _count){
			Name = _name;
			Since = _since;
			To = _to;
			Id = _id;
			Count = _count;
		}
	};
}