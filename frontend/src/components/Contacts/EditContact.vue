<script setup lang="ts">
import { ref } from 'vue'
import { fetchDetail, details } from './ContactDetail'
import Modal from '../Modal/Modal.vue'
import ContactForm from '@/components/Contacts/ContactForm.vue'
import type { Contact } from './Types'

const props = defineProps<{ contact: Contact }>()
const modalId = `EditContactModal-${props.contact.id}`
const showModal = ref(false)
const loading = ref(false)

async function openModal() {
  loading.value = true
  await fetchDetail(props.contact.id) // wait until details are fetched
  loading.value = false
  showModal.value = true
}
</script>

<template>
  <div>
    <button
      @click.prevent="openModal"
      type="button"
      class="btn border border-secondary-subtle btn-sm"
      data-bs-toggle="modal"
      :data-bs-target="`#${modalId}`"
    >
      Edit
    </button>

    <Modal :modal-id="modalId" :is-shown="showModal">
      <template #header>
        Edit Contact: {{ props.contact.name }} {{ props.contact.surname }}
      </template>
      <template #body>
        <ContactForm
          v-if="showModal && !loading"
          :contact="{
            id: props.contact.id,
            name: props.contact.name,
            surname: props.contact.surname,
            email: props.contact.email,
            category: details?.category || '',
            subcategory: details?.subcategory || '',
            phone: details?.phone || '',
            birthDay: details?.birthDay || '',
          }"
        />
        <div v-else>Loading...</div>
      </template>
    </Modal>
  </div>
</template>
