<script setup lang="ts">
import { onMounted, ref } from 'vue'
import ContactDetails from './ContactDetails.vue'
import EditContact from './EditContact.vue'
import { isLogged } from '@/helpers/User'
import type { Contact } from './Types'

const contacts = ref<Contact[]>([])
async function fetchContacts() {
  try {
    const response = await fetch(`api/contact`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    if (response.ok) {
      contacts.value = (await response.json()) as Contact[]
    } else {
      contacts.value = []
    }
  } catch (error) {
    console.log(error)
  }
}

async function deleteContact(id: number) {
  try {
    const response = await fetch(`/api/contact/${id}`, {
      method: 'DELETE',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    if (response.ok) {
      window.location.reload()
    }
  } catch (err) {
    console.error(err)
  }
}

onMounted(async () => {
  fetchContacts()
})
</script>

<template>
  <div class="d-flex justify-content-center align-items-center m-2">
    <ul class="list-group">
      <li v-for="contact in contacts" :key="contact.id" class="list-group-item">
        <div class="d-flex align-items-center justify-content-between">
          <div>
            <p class="mb-1 text-center">
              <i>{{ contact.name }} {{ contact.surname }}</i> {{ contact.email }}
            </p>
          </div>

          <div>
            <div class="details-wrapper d-flex justify-content-center align-items-center">
              <ContactDetails :contact="contact" :contact-id="contact.id" class="m-2" />
              <EditContact v-if="isLogged" :contact="contact" />
              <div v-if="isLogged">
                <button class="btn btn-danger btn-sm ms-2" @click="deleteContact(contact.id)">
                  Delete
                </button>
              </div>
            </div>
          </div>
        </div>
      </li>
    </ul>
  </div>
</template>

<style scoped>
.details-wrapper {
  visibility: hidden;
}

.list-group-item:hover .details-wrapper {
  visibility: visible;
  background-color: white;
}

.list-group-item:hover .details-wrapper:hover {
  background-color: inherit;
}
</style>
