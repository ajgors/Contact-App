<script setup lang="ts">
import { ref } from 'vue'

const props = defineProps<{
  contact?: {
    id: number
    name: string
    surname: string
    email: string
    category: string
    subcategory: string
    phone: string
    birthDay: string
  }
}>()

const addingFailed = ref(false)
const name = ref<string | undefined>(props.contact?.name)
const surname = ref<string | undefined>(props.contact?.surname)
const email = ref<string | undefined>(props.contact?.email)
const category = ref<string | undefined>(props.contact?.category)
const phone = ref<string | undefined>(props.contact?.phone)
const birthDay = ref<string | undefined>(props.contact?.birthDay)
const subCategory = ref<string | undefined>(props.contact?.subcategory)

async function submit() {
  try {
    const result =
      props.contact === undefined
        ? await fetch(`api/contact`, {
            method: 'POST',
            credentials: 'include',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
              name: name.value,
              surname: surname.value,
              email: email.value,
              ContactDetails: {
                category: category.value,
                subcategory: getFinalCategory(),
                phone: phone.value,
                birthDay: birthDay.value,
              },
            }),
          })
        : await fetch(`api/contact/${props.contact.id}`, {
            method: 'PATCH',
            credentials: 'include',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
              name: name.value,
              surname: surname.value,
              category: category.value,
              subcategory: getFinalCategory(),
              phone: phone.value,
              birthDay: birthDay.value,
            }),
          })

    if (!result.ok) {
      console.error('API request failed!')
      return
    }

    const addingSuccess = (await result.json()) as boolean
    addingFailed.value = !addingSuccess

    if (addingSuccess) {
      window.location.href = '/'
    }
  } catch (error) {
    console.log(error)
  }
}

const predefinedBusinessSubcategories = ['Boss', 'Client', 'Colleague']

function getFinalCategory() {
  if (category.value === 'Business') {
    return subCategory.value ?? predefinedBusinessSubcategories[0]
  } else if (category.value === 'Other') {
    return subCategory.value ?? ''
  }

  return undefined
}
</script>

<template>
  <div>
    <form @submit.prevent="submit" class="vstack gap-3 mx-auto" style="max-width: 500px">
      <div>
        <label for="type" class="form-label">Contact Type</label>
        <select v-model="category" id="type" class="form-select" required>
          <option disabled value="">Pick type</option>
          <option value="Business">Business</option>
          <option value="Private">Private</option>
          <option value="Other">Other</option>
        </select>
      </div>

      <div v-if="category === 'Business'">
        <label for="businessSub" class="form-label">Subcategory</label>
        <select v-model="subCategory" id="businessSub" class="form-select" required>
          <option disabled value="">Pick subcategory</option>
          <option v-for="item in predefinedBusinessSubcategories" :key="item" :value="item">
            {{ item }}
          </option>
        </select>
      </div>

      <div v-else-if="category === 'Other'">
        <label for="otherSub" class="form-label">Enter subcategory</label>
        <input v-model="subCategory" type="text" id="otherSub" class="form-control" required />
      </div>

      <div>
        <label for="name" class="form-label">Name</label>
        <input v-model="name" type="text" id="name" class="form-control" required />
      </div>

      <div>
        <label for="surname" class="form-label">Surname</label>
        <input v-model="surname" type="text" id="surname" class="form-control" required />
      </div>

      <div>
        <label for="email" class="form-label">Email</label>
        <input v-model="email" type="email" id="email" class="form-control" required />
      </div>

      <div>
        <label for="phone" class="form-label">Phone</label>
        <input v-model="phone" type="tel" id="phone" class="form-control" required />
      </div>

      <div>
        <label for="birthDay" class="form-label">Birth Date</label>
        <input v-model="birthDay" type="date" id="birthDay" class="form-control" required />
      </div>

      <div class="text-center">
        <button type="submit" class="btn btn-success">
          {{ props.contact ? 'Edit' : 'Add' }}
        </button>
      </div>

      <div v-if="addingFailed" class="text-center">
        <span class="text-danger">This contact already exists.</span>
      </div>
    </form>
  </div>
</template>
