import { ref } from 'vue'

type ContactDetails = {
  contactId: number
  category: string
  subcategory?: string
  phone: string
  birthDay: string
}

export const details = ref<ContactDetails>()

export async function fetchDetail(id: number) {
  try {
    const response = await fetch(`api/contact/${id}/details`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include',
    })

    if (response.ok) {
      details.value = (await response.json()) as ContactDetails
    } else {
    }
  } catch (error) {
    console.log(error)
  }
}
