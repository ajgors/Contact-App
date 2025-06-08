import { ref } from 'vue'

export const isLogged = ref<boolean>(false)

export async function fetchUser() {
  try {
    const response = await fetch(`api/Account/isLogged`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
    })

    const logged = (await response.json()) as boolean

    if (response.ok) {
      isLogged.value = logged
    }
  } catch (error) {
    console.log(error)
  }
}
