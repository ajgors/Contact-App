<script setup lang="ts">
import { onMounted } from 'vue'
import { RouterLink, RouterView } from 'vue-router'
import { fetchUser, isLogged } from '@/helpers/User'

onMounted(async () => {
  fetchUser()
})

async function logOut() {
  try {
    await fetch(`api/Account/logout`, {
      method: 'POST',
      credentials: 'include',
    })

    window.location.href = '/'
  } catch (error) {
    console.log(error)
  }
}
</script>

<template>
  <header>
    <nav
      class="navbar navbar-expand-lg navbar-light bg-light px-4 border-bottom border-secondary-subtle"
    >
      <div class="container-fluid">
        <RouterLink class="navbar-brand" to="/">Contact App</RouterLink>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav ms-auto">
            <li class="nav-item">
              <RouterLink class="nav-link" to="/">Home</RouterLink>
            </li>
            <li v-if="!isLogged" class="nav-item">
              <RouterLink class="nav-link" to="/login">Login</RouterLink>
            </li>
            <li v-else class="nav-item">
              <form @submit.prevent="logOut">
                <button class="btn btn-success ms-2" type="submit">Logout</button>
              </form>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </header>

  <RouterView />
</template>
