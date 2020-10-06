<template>
<div>
<header class="navigation">
    <div class="container">
          
          <nav class="navbar navbar-expand-lg navbar-light">
              <!-- Brand and toggle get grouped for better mobile display -->
              <div class="navbar-header">
                  <a class="navbar-brand" href="index.html">
                      <img src="/images/logo-biale.png" height="50px" alt="Xabe.FFmpeg white logo">
                  </a>
                  <button class="hamburger hamburger--squeeze lg-hidden" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                      <span class="hamburger-box">
                        <span class="hamburger-inner"></span>
                      </span>
                  </button>
              </div>

              <!-- Collect the nav links, forms, and other content for toggling -->
              <div class="collapse navbar-collapse justify-content-center" id="bs-example-navbar-collapse-1">
                <ul class="navbar-nav  navbar-right">
                  <li class="nav-item"><a href="/index.html">Home</a></li>
                  <li class="nav-item"><a href="/pricing.html">Pricing</a></li>
                  <li class="nav-item"><a href="/docs.html">Documentation</a></li>
                  <li class="nav-item"><a href="/faq.html">FAQ</a></li>
                  <li class="nav-item"><a href="/tutorial.html">Get started</a></li>
                </ul>
                  <div></div>
              </div>

          </nav>

    </div>
</header>

  <div>
    <Nuxt />
  </div>
  </div>
</template>

<style>
html {
  font-family: 'Source Sans Pro', -apple-system, BlinkMacSystemFont, 'Segoe UI',
    Roboto, 'Helvetica Neue', Arial, sans-serif;
  font-size: 16px;
  word-spacing: 1px;
  -ms-text-size-adjust: 100%;
  -webkit-text-size-adjust: 100%;
  -moz-osx-font-smoothing: grayscale;
  -webkit-font-smoothing: antialiased;
  box-sizing: border-box;
}

*,
*::before,
*::after {
  box-sizing: border-box;
  margin: 0;
}

.button--green {
  display: inline-block;
  border-radius: 4px;
  border: 1px solid #3b8070;
  color: #3b8070;
  text-decoration: none;
  padding: 10px 30px;
}

.button--green:hover {
  color: #fff;
  background-color: #3b8070;
}

.button--grey {
  display: inline-block;
  border-radius: 4px;
  border: 1px solid #35495e;
  color: #35495e;
  text-decoration: none;
  padding: 10px 30px;
  margin-left: 15px;
}

.button--grey:hover {
  color: #fff;
  background-color: #35495e;
}
</style>
