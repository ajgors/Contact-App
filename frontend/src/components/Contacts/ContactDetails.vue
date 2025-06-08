<script setup lang="ts">
import { ref } from 'vue'
import Modal from '../Modal/Modal.vue'
import { fetchDetail, details } from './ContactDetail'
import type { Contact } from './Types'

const props = defineProps<{ contact: Contact }>()

const modalId = `ContactModal-${props.contact.id}`
const showModal = ref(false)
</script>

<template>
  <div>
    <button
      @click="fetchDetail(props.contact.id)"
      @click.stop.prevent="showModal = true"
      type="button"
      class="btn border border-secondary-subtle btn-sm"
      data-bs-toggle="modal"
      :data-bs-target="`#${modalId}`"
    >
      Details
    </button>

    <Modal :modal-id="modalId" :is-shown="showModal">
      <template #header>{{ props.contact.name }} {{ props.contact.surname }} </template>
      <template #body>
        <div class="container">
          <div class="mb-3 row">
            <label class="col-sm-4 col-form-label fw-semibold">Category:</label>
            <div class="col-sm-8">
              <p class="form-control-plaintext">{{ details?.category }}</p>
            </div>
          </div>

          <div v-if="details?.subcategory" class="mb-3 row">
            <label class="col-sm-4 col-form-label fw-semibold">Subcategory:</label>
            <div class="col-sm-8">
              <p class="form-control-plaintext">{{ details?.subcategory }}</p>
            </div>
          </div>

          <div class="mb-3 row">
            <label class="col-sm-4 col-form-label fw-semibold">Phone:</label>
            <div class="col-sm-8">
              <p class="form-control-plaintext">{{ details?.phone }}</p>
            </div>
          </div>

          <div class="mb-3 row">
            <label class="col-sm-4 col-form-label fw-semibold">Email:</label>
            <div class="col-sm-8">
              <p class="form-control-plaintext">{{ props.contact.email }}</p>
            </div>
          </div>

          <div class="mb-3 row">
            <label class="col-sm-4 col-form-label fw-semibold">Birth date:</label>
            <div class="col-sm-8">
              <p class="form-control-plaintext">{{ details?.birthDay }}</p>
            </div>
          </div>
        </div>
      </template>
    </Modal>
  </div>
</template>
