<script setup lang="ts">
import { ref } from 'vue'

const email = ref<string>('')
const password = ref<string>('')
const loginFailed = ref<boolean>(false)

async function submit() {
  try {
    const result = await fetch(`api/Account/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email: email.value.trim(),
        password: password.value,
      }),
      credentials: 'include',
    })

    if (!result.ok) {
      console.error('API request failed!')
      return
    }

    const loginSuccess = (await result.json()) as boolean
    loginFailed.value = !loginSuccess
    password.value = ''

    if (loginSuccess) {
      window.location.href = '/'
    }
  } catch (error) {
    console.log(error)
  }
}
</script>

<template>
  <div class="m-5">
    <h2>login: user@example.com</h2>
    <h2>password: User1234!</h2>
    <form @submit.prevent="submit" class="vstack gap-3 mx-auto" style="max-width: 500px">
      <div>
        <label for="email" class="form-label">Email</label>
        <input
          v-model="email"
          type="email"
          id="email"
          class="form-control"
          placeholder="Email"
          required
        />
      </div>

      <div>
        <label for="password" class="form-label">{{ 'Password' }}</label>
        <input
          v-model="password"
          type="password"
          id="password"
          class="form-control"
          :placeholder="`Password`"
          required
        />
      </div>

      <div class="text-center">
        <button type="submit" class="btn btn-success">Sign in</button>
      </div>

      <div v-if="loginFailed" class="text-center text-danger">Login failed.</div>
    </form>
  </div>
</template>

<style scoped>
h2 {
  text-align: center;
}
</style>
