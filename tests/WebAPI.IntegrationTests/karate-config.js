function fn() {
  var env = karate.env;

  if (!env) {
    env = 'dev';
  }

  var config = {
    urlBase: 'https://localhost:5001/api/',
  };

  karate.configure('connectTimeout', 5000);
  karate.configure('readTimeout', 5000);
  return config;
}
